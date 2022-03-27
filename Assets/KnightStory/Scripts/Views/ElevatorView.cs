using UnityEngine;

public class ElevatorView : MonoBehaviour, InteractiveObject
{
    [SerializeField]
    private SliderJoint2D _sliderJoint;

    private JointMotor2D _jointMotor;
    private float _moveTime = 0;
    private float _delay = 6f;

    private void Start()
    {
        _jointMotor = _sliderJoint.motor;
    }

    private void FixedUpdate()
    {
        ElevatorMove();
    }

    private void ElevatorMove()
    {
        if (_moveTime > Time.deltaTime)
        {
            _moveTime -= Time.deltaTime;
        } else
        {
            _moveTime = _delay;
            _jointMotor.motorSpeed *= -1;
            _sliderJoint.motor = _jointMotor;
        }
    }
}
