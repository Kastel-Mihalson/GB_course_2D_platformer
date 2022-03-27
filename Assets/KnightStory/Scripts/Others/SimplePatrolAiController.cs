using UnityEngine;

public class SimplePatrolAiController
{
    private readonly EnemyView _view;
    private readonly AiConfig _config;

    private Transform _target;
    private int _currentPointIndex;

    public SimplePatrolAiController(EnemyView view, AiConfig config)
    {
        _view = view;
        _config = config;
        _target = GetNextWayPoint();
    }

    public void FixedUpdate()
    {
        _view.Rigidbody.velocity = CalcVelocity(_view.transform.position);
    }

    public Vector2 CalcVelocity(Vector2 fromPosition)
    {
        var distance = Vector2.Distance(_target.position, fromPosition);

        if (distance <= _config.MinDistanceToTarget)
            _target = GetNextWayPoint();

        var direction = ((Vector2)_target.position - fromPosition).normalized;
        return _config.Speed * direction;
    }

    private Transform GetNextWayPoint()
    {
        _currentPointIndex = (_currentPointIndex + 1) % _config.WayPoints.Length;
        return _config.WayPoints[_currentPointIndex];
    }
}
