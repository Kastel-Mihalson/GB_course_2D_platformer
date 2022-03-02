using UnityEngine;

public class PlayerView : MonoBehaviour
{
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
}
