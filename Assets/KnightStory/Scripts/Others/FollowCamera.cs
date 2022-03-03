using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private float _cameraSpeed = 2f;

    private void Awake()
    {
        transform.position = SetTargetPos();
    }

    private void Update()
    {
        Vector3 target = SetTargetPos();
        Vector3 pos = Vector3.Lerp(transform.position, target, _cameraSpeed * Time.deltaTime);

        transform.position = pos;
    }

    private Vector3 SetTargetPos()
    {
        return new Vector3()
        {
            x = _playerTransform.position.x,
            y = _playerTransform.position.y + 2,
            z = _playerTransform.position.z - 10
        };
    }
}
