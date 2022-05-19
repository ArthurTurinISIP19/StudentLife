using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    public  event UnityAction<bool> IsAccelerometerLost;


    [SerializeField] private Slider slider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector3 _direction;

    private float _incline = 0f;
    private float _sign = 1f;
    private float _maxValue;
    private float _minValue;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        StartCoroutine(Wait2s());
        StartCoroutine(ChangeKoef());
    }

    private void FixedUpdate()
    {
        if (CameraSwitcherMain.GameActivated)
        {
            _animator.enabled = true;
            _animator.Play("GamePlay");
            Move();
            CheckSlider();
        }
    }

    private void Move()
    {
        _direction = Input.acceleration * 100;
        _rigidbody.velocity = new Vector3(_direction.x + _incline, 0f, 0f);
    }

    private void CheckSlider()
    {
        slider.value = transform.position.x;
        if (transform.position.x > 45 || transform.position.x < -45)
        {
            GameResult(true);
        }
    }


    public void GameResult(bool isGameLost)
    {
        IsAccelerometerLost?.Invoke(isGameLost);
        gameObject.SetActive(false);
    }

    IEnumerator Wait2s()
    {
        _maxValue = Random.Range(25f, 45f);
        _minValue = Random.Range(-25f, -45f);
        yield return new WaitForSeconds(3f);
        _sign *= -1;
        StartCoroutine(Wait2s());
    }

    IEnumerator ChangeKoef()
    {
        
        yield return new WaitForFixedUpdate();

        if (_sign >= 0 && _incline <= _maxValue)
        {
            _incline += _sign;
        }
        else if (_sign <= 0 && _incline > _minValue)
        {
            _incline += _sign;
        }
        StartCoroutine(ChangeKoef());
    }


}
