using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action<int> OnHealthChanged;

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 25.0f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioSource attackSound;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    //private AudioSource _audioSource;
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private bool _jumpPressed;
    private int _health = 100;
    private float _lastX;

    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        //_audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetBool(Grounded, _isGrounded);
        _animator.SetBool(Moving, horizontal != 0);
        if (horizontal != 0) _lastX = horizontal;
        _spriteRenderer.flipX = _lastX > 0 && Mathf.Abs(_lastX) > 0.1f;
        if (Input.GetKeyDown(KeyCode.Space)) _jumpPressed = true;

        // Attack
        if (Input.GetKeyDown(KeyCode.X))
        {
            var enemy = Physics2D.OverlapBox(transform.position, Vector2.one, 0f, enemyLayer);
            if (enemy != null) {
                attackSound.Play();
                Destroy(enemy.gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var velocity = new Vector2(x * speed, _rb.linearVelocity.y);
        _rb.linearVelocity = velocity;

        CheckGrounded();

        if (!_jumpPressed || !_isGrounded) return;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
        _jumpPressed = false;
    }

    private void CheckGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f);
        var newIsGrounded = hit.collider != null && hit.collider.CompareTag("Ground");
        _isGrounded = newIsGrounded;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            transform.SetParent(other.transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            transform.SetParent(null);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
    }
}
