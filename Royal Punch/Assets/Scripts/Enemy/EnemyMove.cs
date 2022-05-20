using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform _enemy;

    private void Start()
    {
        _enemy = transform;
    }

    private void FixedUpdate()
    {
        _enemy.rotation = Quaternion.Euler(0, _enemy.rotation.y, 0);
    }
}
