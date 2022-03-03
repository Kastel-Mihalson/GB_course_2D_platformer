using UnityEngine;

public class PlayerView : MonoBehaviour
{
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

    public float WalkSpeed => _walkSpeed;

    public float JumpStartSpeed => _jumpStartSpeed;

    public float MovingTresh => _movingTresh;

    public float FlyTresh => _flyTresh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactivObject = collision.GetComponent<InteractiveObject>();

        if (interactivObject is SoulView)
        {
            Destroy(collision.gameObject);
        }
        if (interactivObject is DeathLineView)
        {
            transform.position = _playerStartPos.position;
        }
        if (interactivObject is ThornTrapView)
        {
            Debug.Log("Ouch!");
            transform.position = _playerStartPos.position;
        }
        if (interactivObject is CheckPointView)
        {
            _playerStartPos.position = collision.transform.position.Change(y: collision.transform.position.y / 2);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Checkpoint! Save player position");
        }
        if (interactivObject is LevelEndView)
        {
            var levelEndView = (LevelEndView)interactivObject;

            levelEndView.ShowCanvas();
            Debug.Log("Level passed!");
        }
    }
}
