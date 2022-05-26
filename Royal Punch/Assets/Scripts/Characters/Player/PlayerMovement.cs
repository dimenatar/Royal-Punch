using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private SimpleTouchController _controller;
    [SerializeField] private Transform _enemy;

    [SerializeField] private float _speed = 5;

    private Transform _playerTransform;

    public Vector3 DraggingForce { get; set; } = Vector3.zero;

    private void Awake()
    {
        _controller.TouchEvent += MovePlayer;
       _playerTransform = transform;
    }

    private void FixedUpdate()
    {
        //Debug.Log(_playerTransform.forward);
        _player.velocity = SetVelocityBasedOnRotation(ConvertYVelocityToZ(_controller.GetTouchPosition)) + DraggingForce;

        _playerTransform.LookAt(_enemy);
        _playerTransform.eulerAngles = new Vector3(0, _playerTransform.eulerAngles.y, 0);
    }

    private Vector3 ConvertYVelocityToZ(Vector2 touchPos) => new Vector3(touchPos.x, 0, touchPos.y);

    private Vector3 SetVelocityBasedOnRotation(Vector3 direction)
    {
        Vector3 forward = _speed * direction.z * _playerTransform.forward;
        Vector3 side = _speed * direction.x * _playerTransform.right;
        return forward + side;
    }

    private void MovePlayer(Vector2 direction)
    {
        //_player.velocity = SetVelocityBasedOnRotation(ConvertYVelocityToZ(direction)) + DraggingForce;
        //Debug.Log(direction);
        //_player.velocity = direction;
    }
}
