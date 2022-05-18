using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private SimpleTouchController _controller;

    public Vector3 DraggingForce { get; set; } = Vector3.zero;

    private void Awake()
    {
        _controller.TouchEvent += MovePlayer;
       
    }

    private void FixedUpdate()
    {
        _player.velocity = ConvertYVelocityToZ(_controller.GetTouchPosition) * 5 + DraggingForce;
    }

    private Vector3 ConvertYVelocityToZ(Vector2 touchPos) => new Vector3(touchPos.x, 0, touchPos.y);

    private void MovePlayer(Vector2 direction)
    {
        Debug.Log(direction);
        _player.velocity = direction;
    }
}
