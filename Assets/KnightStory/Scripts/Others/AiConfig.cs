using System;
using UnityEngine;

[Serializable]
public struct AiConfig
{
    public float Speed;
    public float MinDistanceToTarget;
    public Transform[] WayPoints;
}
