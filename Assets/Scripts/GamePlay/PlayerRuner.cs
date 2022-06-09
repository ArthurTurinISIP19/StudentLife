using UnityEngine;
using UnityEngine.Events;

public class PlayerRuner : AbstractGameResult
{
    [Header("Увеличивать у врага на 5 ")]
    [SerializeField] private int Speed = 50;
    [SerializeField] private float _time;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _multiplier = 1f;
    public static float _multiplierStep = 0.04f;

    public override float LevelTime { get => _time; set => _time = value; }

    public override event UnityAction<bool> GameLost;

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
        if (CameraSwitcher.GameActivated)
        {
            _rigidbody2D.transform.Translate(Vector2.right * Speed * _multiplier * Time.deltaTime);
        }
    }
    public void IncreaseSpeed()
    {
        if (CameraSwitcher.GameActivated)
        {
            if(Input.touchCount == 1)
            {
                _multiplier += _multiplierStep;
                if(_animator.speed < 2)
                {
                    _animator.speed += 0.1f;
                }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Lose>())
        {
            GameResult(true);
        }
        else if (collision.GetComponent<Win>())
        {
            GameResult(false);
        }
    }

    public override void GameResult(bool isGameLost)
    {
        GameLost?.Invoke(isGameLost);
    }
}
