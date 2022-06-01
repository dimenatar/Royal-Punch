using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private float _delayToHit;
    [SerializeField] private Character _enemy;
    [SerializeField] private Character _player;
    [SerializeField] private EnemyFight _enemyFight;
    [SerializeField] private Ragdoll _playerRagdoll;
    [SerializeField] private DamageMultiplier _damageMultiplier;

    public event Action OnEnemyEntersTrigger;
    public event Action OnEnemyExitsTrigger;

    private bool _isInFight;
    private int _damage;

    private void Awake()
    {
        OnEnemyEntersTrigger += () => StartCoroutine(nameof(HitEnemy));
        OnEnemyEntersTrigger += _damageMultiplier.StartAddind;

        OnEnemyExitsTrigger += () => StopCoroutine(nameof(HitEnemy));
        OnEnemyExitsTrigger += _damageMultiplier.StopAdding;

        _player.OnFallen += () => StopFight();
        _playerRagdoll.OnStandedUp += TryStartFight;
        _player.OnDied += StopFight;
    }

    public void Initialise(int damage)
    {
        _damage = damage;
        _isInFight = false;
    }

    public void ForceReset()
    {
        StopCoroutine(nameof(HitEnemy));
        _enemy.GetComponent<EnemyFight>().IsInTriggerWithPlayer = false;
        OnEnemyExitsTrigger?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyFight>() && !_player.IsHitted && !_isInFight)
        {
            StartFight();
        }
        else if (other.GetComponent<EnemyFight>() && _player.IsHitted)
        {
            other.GetComponent<EnemyFight>().IsInTriggerWithPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyFight>())
        {
            StopFight();
        }
    }

    private void StartFight()
    {
       // print("START FIGHT");
        _enemyFight.GetComponent<EnemyFight>().IsInTriggerWithPlayer = true;
        _enemyFight.GetComponent<EnemyFight>().StartFightWithPlayer();
        OnEnemyEntersTrigger?.Invoke();
        _isInFight = true;
    }

    private void StopFight()
    {
        if (_isInFight)
        {
            //print("END FIGHT");
            _enemyFight.IsInTriggerWithPlayer = false;
            _enemyFight.StopFightWithPlayer();
            OnEnemyExitsTrigger?.Invoke();
            _isInFight = false;
        }
    }

    private IEnumerator HitEnemy()
    {
        while (true)
        {
            _enemy.TakeDamage((int)(_damage * _damageMultiplier.Multiplier));
            yield return new WaitForSeconds(_delayToHit);
        }
    }

    private void TryStartFight()
    {
        if (_isInFight)
        {
            StartFight();
        }
    }
}
