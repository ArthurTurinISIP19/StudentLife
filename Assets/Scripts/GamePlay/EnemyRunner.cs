using UnityEngine;

public class EnemyRunner : MonoBehaviour
{
    [Header("Увеличивать у врага на 5 ")]
    [SerializeField] private int Speed = 53;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private float _multiplier = 1f;
    public static float _addMultiplier = 0.002f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        IncreaseSpeed();
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
        _multiplier += _addMultiplier;
        _animator.speed += 0.001f;
    }
}

