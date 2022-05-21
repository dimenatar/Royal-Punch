using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private Character _enemy;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Animator _playerAnimator;

    [SerializeField] private Transform _playerStartPoint;

    public void ResetLevel()
    {
        _playerTransform.position = _playerStartPoint.position;

    }
}
