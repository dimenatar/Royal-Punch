using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundResult : MonoBehaviour
{
    [SerializeField] private Character _enemy;
    [SerializeField] private Character _player;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private EnemyAnimations _enemyAnimations;

    [SerializeField] private EnemyFight _enemyFight;
    [SerializeField] private PlayerFight _playerFight;

    [SerializeField] private TouchPosition _touchPosition;

    private void Awake()
    {
        _enemy.OnDied += EnemyDied;
        _player.OnDied += PlayerDied;
    }

    private void EndRound(bool win)
    {
        _touchPosition.DisalbeTouch();
        if (win)
        {
            _playerAnimations.Win();
        }
        else
        {

        }
        _enemyFight.StopFightWithPlayer();
        _playerFight.ForceReset();
        _enemyAnimations.ForceStop();
    }

    private void PlayerDied() => EndRound(false);
    private void EnemyDied() => EndRound(true);
}
