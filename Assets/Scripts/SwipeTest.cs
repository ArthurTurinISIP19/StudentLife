using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeTest : AbstractProgress
{
    [SerializeField] private bool _isMobile = true;
    [SerializeField] private bool _swipeRight;
    [SerializeField] private bool _swipeLeft;
 
    private Animator _animator;
    private bool _isPlaying = false;

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;
    private float _deadZone = 80f;
    private bool _isSwiping;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwipeStart()
    {
        _isSwiping = true;
        _tapPosition = Input.mousePosition;

        if (_isMobile)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = Input.GetTouch(0).position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    CheckSwipe();
                    ResetSwipe();
                }
            }
        }
    }
    public void SwipeEnd()
    {
        CheckSwipe();
        ResetSwipe();
    }

    private void CheckSwipe()
    {
        if (_isSwiping)
        {
            _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;  
        }

        if (_isMobile)
        {
            _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > _deadZone)
        {
            if(Math.Abs(_swipeDelta.x) > Math.Abs(_swipeDelta.y))
            {
                if(_swipeDelta.x > 0 && _swipeRight && CameraSwitcherMain.GameActivated)
                {
                    ProgressUp();
                    PlayAnimation();
                }
                else if (_swipeDelta.x < 0 && _swipeLeft && CameraSwitcherMain.GameActivated)
                {
                    ProgressUp();
                    PlayAnimation();
                }
            }

        }

        ResetSwipe();
    }

    private void ResetSwipe()
    {
        _isSwiping = false;
        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }

    private void PlayAnimation()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
            _animator.speed = 2;
            _animator.Play("GamePlay");
            StartCoroutine(AnimationControl());
        }
    }

    public IEnumerator AnimationControl()
    {

        yield return new WaitForSeconds(0.5f);
        if (CameraSwitcherMain._isGameActivated)
        {
            _animator.speed = 0f;
            _isPlaying = false;
        }
    }
}
