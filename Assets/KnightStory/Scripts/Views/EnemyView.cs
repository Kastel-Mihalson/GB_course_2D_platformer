using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
}
