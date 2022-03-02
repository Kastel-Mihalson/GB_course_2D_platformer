using UnityEngine;

public class PlayerController
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    private PlayerView _playerView;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private ContactsPoller _contactsPoller;
    private Animator _animator;

    private float _horizontal;
    private bool _doJump;
    private bool _attack;
    private bool _rolling;

    public PlayerController(PlayerView playerV2View)
    {
        _playerView = playerV2View;
        _rigidbody = playerV2View.GetComponent<Rigidbody2D>();
        _spriteRenderer = playerV2View.GetComponent<SpriteRenderer>();
        _animator = playerV2View.GetComponent<Animator>();
        _contactsPoller = new ContactsPoller(playerV2View.GetComponent<Collider2D>());
    }

    public void Update()
    {
        _horizontal = Input.GetAxis(Horizontal);
        _doJump = Input.GetAxis(Vertical) > 0;
        _attack = Input.GetKey(KeyCode.Mouse0);
        _rolling = Input.GetKey(KeyCode.LeftShift);
    }

    public void FixedUpdate()
    {
        var isGoSideWay = Mathf.Abs(_horizontal) > _playerView.MovingTresh;
        var newVelocity = 0f;

        _contactsPoller.Update();

        if (isGoSideWay) _spriteRenderer.flipX = _horizontal < 0;
        if (isGoSideWay &&
            (_horizontal > 0 || !_contactsPoller.HasLeftContacts) &&
            (_horizontal < 0 || !_contactsPoller.HasRightContacts))
        {
            _animator.SetBool("IsRunning", true);
            newVelocity = Time.fixedDeltaTime * _playerView.WalkSpeed * (_horizontal < 0 ? -1 : 1);
        } else
        {
            _animator.SetBool("IsRunning", false);
        }

        _rigidbody.velocity = _rigidbody.velocity.Change(x: newVelocity);

        if (_contactsPoller.IsGrounded && _doJump && Mathf.Abs(_rigidbody.velocity.y) <= _playerView.FlyTresh)
        {
            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector2.up * _playerView.JumpStartSpeed);
        }
        if (_attack)
        {
            _animator.SetTrigger("Attack");
        }
        if (_rolling)
        {
            _animator.SetTrigger("Rolling");
        }
    }
}
