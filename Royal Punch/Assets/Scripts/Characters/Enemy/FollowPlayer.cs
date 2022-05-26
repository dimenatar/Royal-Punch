using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] EnemySpecial _enemySpecial;

    [SerializeField] private float _rotationSpeed;

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
            //_enemy.LookAt(_player);
            //_enemy.rotation = Quaternion.Euler(Vector3.Lerp(_enemy.rotation.eulerAngles, )
            Vector3 direcion = (_player.position - _enemy.position).normalized;
            var tagetRotation = Quaternion.LookRotation(direcion);

            _enemy.rotation = Quaternion.RotateTowards(_enemy.rotation, tagetRotation, Time.deltaTime * _rotationSpeed);

            //_enemy.eulerAngles = new Vector3(_enemy.eulerAngles.x, 0, _enemy.eulerAngles.z);
            _enemy.eulerAngles = new Vector3(0, _enemy.eulerAngles.y, 0);
        }
    }

    public void StopFollowing() => _isFollowing = false;
    public void StartFollowing() => _isFollowing = true;

}
