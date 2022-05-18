using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecial : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Timer _timer;

    [SerializeField] private float _draggingPlayerForce = 1;

    private void PickRandomSpecialAttack()
    {

    }

    private IEnumerator StartDraggingPlayer()
    {
        while (true)
        {
            _player.DraggingForce = (transform.position - _playerTransform.position) * _draggingPlayerForce;
            yield return null;
        }
    }
}
