using UnityEngine;
using UnityEngine.Events;

public class PlayerRunnerJump : AbstractGameResult
{
    [Header("Увеличивать у врага на 5 ")]
    [SerializeField] private int Speed = 50;
    [SerializeField] private float _time;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _multiplier = 1f;
    private bool _isGrounded;
    private Vector3 jump = new Vector3(0, 100, 0);

    public override float LevelTime { get => _time; set => _time = value; }

    public override event UnityAction<bool> GameLost;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        if (CameraSwitcherMain.GameActivated)
        {
            _rigidbody2D.transform.Translate(Vector2.right * Speed * _multiplier * Time.deltaTime);
        }
    }
    public void Jump()
    {
        if (CameraSwitcherMain.GameActivated)
        {
            if (_isGrounded)
            {
                _rigidbody2D.AddForce(jump, ForceMode2D.Impulse);
                _isGrounded = false;
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    public override void GameResult(bool isGameLost)
    {
        GameLost?.Invoke(isGameLost);
    }
}
