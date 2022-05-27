using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] private float _rotationSpeed;

    private bool _isFollowing = true;
    private Transform _enemy;

    private void Awake()
    {
        _enemy = transform;
    }

    private void FixedUpdate()
    {
        
        if (_isFollowing)
        {
            Vector3 direcion = (_player.position - _enemy.position).normalized;
            var tagetRotation = Quaternion.LookRotation(direcion);

            _enemy.rotation = Quaternion.RotateTowards(_enemy.rotation, tagetRotation, Time.deltaTime * _rotationSpeed);

            _enemy.eulerAngles = new Vector3(0, _enemy.eulerAngles.y, 0);
        }
    }

    public void StopFollowing() => _isFollowing = false;
    public void StartFollowing() => _isFollowing = true;

}
