using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] EnemySpecial _enemySpecial;

    private bool _isFollowing = true;
    private Transform _enemy;

    private void Awake()
    {
        _enemySpecial.OnSpecialAttackPicked += (attack) => _isFollowing = false;
        _enemySpecial.OnSpecialAttackPicked += (attack) => _isFollowing = true;
        _enemy = transform;
    }

    private void FixedUpdate()
    {
        if (_isFollowing)
        {
            _enemy.LookAt(_player);
        }
    }
}