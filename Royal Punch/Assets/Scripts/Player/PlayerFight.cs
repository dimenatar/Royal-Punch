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

    public event Action OnEnemyEntersTrigger;
    public event Action OnEnemyExitsTrigger;

    private bool _isInFight;
    private int _damage;

    private void Awake()
    {
        OnEnemyEntersTrigger += () => StartCoroutine(nameof(HitEnemy));
        OnEnemyExitsTrigger += () => StopCoroutine(nameof(HitEnemy));
        _player.OnHitted += () => StopFight();
        _playerRagdoll.OnStandedUp += StartFight;
    }

    public void Initialise(int damage)
    {
        _damage = damage;
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
        print("START FIGHT");
        _enemyFight.GetComponent<EnemyFight>().IsInTriggerWithPlayer = true;
        _enemyFight.GetComponent<EnemyFight>().StartFightWithPlayer();
        OnEnemyEntersTrigger?.Invoke();
        _isInFight = true;
    }

    private void StopFight()
    {
        if (_isInFight)
        {
            print("END FIGHT");
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
            _enemy.TakeDamage(_damage);
            yield return new WaitForSeconds(_delayToHit);
        }
    }
}
