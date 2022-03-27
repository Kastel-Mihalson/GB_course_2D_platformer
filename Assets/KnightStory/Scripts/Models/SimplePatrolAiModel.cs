using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrolAiModel
{
    private readonly AiConfig _config;
    private Transform _target;
    private int _currentPointIndex;

    public SimplePatrolAiModel(AiConfig config)
    {
        _config = config;
        _target = GetNextWayPoint();
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
