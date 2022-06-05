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

    [SerializeField] private Transform _armature;
    [SerializeField] private Transform _spine;

    [SerializeField] private float _backwardsBorder;

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

        //var dir = (_enemy.position - _spine.position).normalized;
        //var lookRotation = Quaternion.LookRotation(dir);

        //_spine.rotation = lookRotation;

        // _spine.LookAt(_enemy);
        // _spine.eulerAngles = new Vector3(0, _spine.eulerAngles.y, 0);

        //float angle = 0;
        //if (_controller.GetTouchPosition.y >=0)
        //angle = Mathf.Clamp(Mathf.Atan2(_controller.GetTouchPosition.x, _controller.GetTouchPosition.y) * Mathf.Rad2Deg, -90, 90);
        //else
        //angle = _controller.GetTouchPosition.x * 90;

        //_armature.transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
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

    private float Clamp(float value, float min, float max)
    {
        if (value >= min && value <= max) return value;
        else if (value < min) return min;
        else return max;
    }
}
