using System.Collections;
using UnityEngine;

public class TapperProgress : AbstractProgress
{
    [SerializeField] private float _animatorSpeed;
    private Animator _animator;
    private bool _isPlaying = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Button()
    {
        ProgressUp();
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        if (!_isPlaying)
        {
            _isPlaying = true;
            _animator.speed = _animatorSpeed;
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
