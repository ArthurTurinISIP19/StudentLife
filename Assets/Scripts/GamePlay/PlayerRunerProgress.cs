using UnityEngine;
using UnityEngine.Events;

public class PlayerRunerProgress : AbstractProgress
{
    [Header("Увеличивать у врага на 5 ")]
    [SerializeField] private int Speed = 50;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _multiplier = 1f;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        DecreaseSpeed();
        Move();
    }
    private void Move()
    {
        if (CameraSwitcherMain.GameActivated)
        {
            _rigidbody2D.transform.Translate(Vector2.right * Speed * _multiplier * Time.deltaTime);
        }
    }
    public void IncreaseSpeed()
    {
        if (CameraSwitcherMain.GameActivated)
        {
            if (Input.touchCount == 1)
            {
                _multiplier += 0.05f;

                if (_animator.speed < 2)
                {
                    _animator.speed += 0.1f;
                }

                ProgressUp();
            }
        }
    }
    private void DecreaseSpeed()
    {
        if (_multiplier > 1f)
        {
            _multiplier -= 0.002f;
        }
        if (_animator.speed > 1)
        {
            _animator.speed -= 0.005f;
        }
    }
}
