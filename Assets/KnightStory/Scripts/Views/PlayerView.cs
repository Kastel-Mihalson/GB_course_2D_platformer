using UnityEngine;

public class PlayerView : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Collider2D _deadLine;

    [SerializeField]
    private Transform _playerStartPos;

    [SerializeField]
    private float _walkSpeed = 60f;

    [SerializeField]
    private float _jumpStartSpeed = 200f;

    [SerializeField]
    private float _movingTresh = 0.1f;

    [SerializeField]
    private float _flyTresh = 0.3f;

    public Rigidbody2D Rigidbody => _rigidbody;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    public Animator Animator => _animator;

    public float WalkSpeed => _walkSpeed;

    public float JumpStartSpeed => _jumpStartSpeed;

    public float MovingTresh => _movingTresh;

    public float FlyTresh => _flyTresh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactivObject = collision.GetComponent<InteractiveObject>();
        
        if (interactivObject is InteractiveObject)
        {
            switch (interactivObject.GetType().Name)
            {
                case nameof(SoulView):
                    Destroy(collision.gameObject);
                    break;

                case nameof(DeathLineView):
                    transform.position = _playerStartPos.position;
                    break;

                case nameof(ThornTrapView):
                    Debug.Log("Ouch!");
                    transform.position = _playerStartPos.position;
                    break;

                case nameof(CheckPointView):
                    _playerStartPos.position = collision.transform.position.Change(y: collision.transform.position.y / 2);
                    collision.gameObject.GetComponent<Collider2D>().enabled = false;
                    Debug.Log("Checkpoint! Save player position");
                    break;

                case nameof(LevelEndView):
                    LevelEndView levelEndView = (LevelEndView)interactivObject;
                    levelEndView.ShowCanvas();
                    Debug.Log("Level passed!");
                    break;

                default:
                    break;
            }
        } else
        {

        }
    }
}
