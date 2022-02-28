using UnityEngine;

public class PlayerController
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    private float _yVelocity;

    private PlayerView _playerView;
    private SpritesAnimator _spritesAnimator;

    public PlayerController(PlayerView playerView, SpritesAnimator spritesAnimator)
    {
        _playerView = playerView;
        _spritesAnimator = spritesAnimator;
    }

    public void Update()
    {
        var doJump = Input.GetAxis(Vertical) > 0;
        var xAxisInput = Input.GetAxis(Horizontal);
        var isGoSideWay = Mathf.Abs(xAxisInput) > _playerView.MovingTresh;

        if (isGoSideWay) GoSideWay(xAxisInput);

        if (IsGrounded())
        {
            _spritesAnimator.StartAnim(_playerView.SpriteRenderer, 
                isGoSideWay ? AnimState.Run : AnimState.Idle, true, _playerView.AnimationsSpeed);

            if (doJump && Mathf.Approximately(_yVelocity, 0))
            {
                _yVelocity = _playerView.JumpStartSpeed;
            } 
            else if (_yVelocity < 0)
            {
                _yVelocity = 0;
                MovementCharacter();
            }
        } else
        {
            LandingCharater();
        }
    }

    private void LandingCharater()
    {
        _yVelocity += _playerView.Acceleration * Time.deltaTime;

        if (Mathf.Abs(_yVelocity) > _playerView.FlyTresh)
            _spritesAnimator.StartAnim(_playerView.SpriteRenderer, AnimState.Jump, true, _playerView.AnimationsSpeed);

        _playerView.transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
    }

    private void MovementCharacter()
    {
        _playerView.transform.position = _playerView.transform.position.Change(y: _playerView.GroundLevel);
    }

    private bool IsGrounded()
    {
        return _playerView.transform.position.y <= _playerView.GroundLevel && _yVelocity <= 0;
    }

    private void GoSideWay(float xAxisInput)
    {
        _playerView.transform.position += Vector3.right * (Time.deltaTime * _playerView.WalkSpeed * (xAxisInput < 0 ? -1 : 1));
        _playerView.SpriteRenderer.flipX = xAxisInput < 0;
    }
}
