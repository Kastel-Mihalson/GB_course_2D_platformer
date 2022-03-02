using UnityEngine;

public class TurretController
{
    private TurretView _turretView;
    private Transform _arrowSpawner;
    private Transform _target;
    private GameObject _arrowPrefab;
    private Rigidbody2D _rigidbody;
    private GameObject _arrowGO;

    private float ShootForce = 300f;
    private float _delay = 2f;
    private float _arrowSpawnTime = 0f;

    public TurretController(TurretView turretView, Transform target)
    {
        _turretView = turretView;
        _target = target;
        _arrowSpawner = turretView.GetComponentInChildren<ArrowSpawnerMarker>().transform;
        _arrowPrefab = Resources.Load<GameObject>("Arrow");
    }

    public void Update()
    {
        TurretRotate();
    }

    private void TurretRotate()
    {
        var dir = _target.position - _turretView.transform.position;
        var angle = Vector3.Angle(-Vector3.up, dir);
        var axis = Vector3.Cross(-Vector3.up, dir);

        _turretView.transform.rotation = Quaternion.AngleAxis(angle, axis);
    }

    public void FixedUpdate()
    {
        if (_arrowSpawnTime > Time.deltaTime)
        {
            _arrowSpawnTime -= Time.deltaTime;
        } else
        {
            if (_arrowGO != null) Destroy();

            _arrowSpawnTime = _delay;

            _arrowGO = Object.Instantiate(_arrowPrefab, _arrowSpawner.position, Quaternion.identity);
            _rigidbody = _arrowGO.GetComponent<Rigidbody2D>();

            var dir = _target.position - _arrowGO.transform.position;
            var angle = Vector3.Angle(-Vector3.left, dir);
            var axis = Vector3.Cross(-Vector3.left, dir);

            _arrowGO.transform.rotation = Quaternion.AngleAxis(angle, axis);

            _rigidbody.AddForce(dir.normalized * ShootForce);
        }
    }

    public void Destroy()
    {
        Object.Destroy(_arrowGO);
    }
}
