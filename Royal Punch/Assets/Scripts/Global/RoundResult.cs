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
    [SerializeField] private EnemySpecial _enemySpecial;

    [SerializeField] private TouchPosition _touchPosition;

    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private Animator _specialAnimator;

    [SerializeField] private GameObject _anim1;
    [SerializeField] private GameObject _anim2;
    [SerializeField] private GameObject _playerHealthBar;

    [SerializeField] private LevelStageController _levelStageController;
    [SerializeField] private Ragdoll _enemyRagdoll;
    [SerializeField] private Ragdoll _playerRagdoll;


    private void Awake()
    {
        _enemy.OnDied += EnemyDied;
        _player.OnDied += PlayerDied;
    }

    private void EndRound(bool win)
    {
        _enemySpecial.StopSpecials();
        _enemyAnimator.enabled = false;
        _specialAnimator.enabled = false;
        _anim1.SetActive(false);
        _anim2.SetActive(false);

        _playerHealthBar.SetActive(false);

        _touchPosition.DisalbeTouch();
        if (win)
        {
            _playerAnimations.Win();
            _levelStageController.UpgrageStage();
            _enemyRagdoll.FullyFall();
        }
        else
        {
            _playerRagdoll.FullyFall();
        }
        _enemyFight.StopFightWithPlayer();
        _playerFight.ForceReset();
        //_enemyAnimations.ForceStop();
    }

    private void PlayerDied() => EndRound(false);
    private void EnemyDied() => EndRound(true);
}
