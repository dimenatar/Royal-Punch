using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    [SerializeField] private float _delayToHit;
    [SerializeField] private Character _enemy;
    [SerializeField] private Character _player;

    public event Action OnEnemyEntersTrigger;
    public event Action OnEnemyExitsTrigger;

    private int _damage;

    private void Awake()
    {
        OnEnemyEntersTrigger += () => StartCoroutine(nameof(HitEnemy));
        OnEnemyExitsTrigger += () => StopCoroutine(nameof(HitEnemy));
    }

    public void Initialise(int damage)
    {
        _damage = damage;
    }

    public void ForceReset()
    {
        StopCoroutine(nameof(HitEnemy));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyFight>())
        {
            other.GetComponent<EnemyFight>().IsInTriggerWithPlayer = true;
            other.GetComponent<EnemyFight>().StartFightWithPlayer();
            OnEnemyEntersTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyFight>())
        {
            other.GetComponent<EnemyFight>().IsInTriggerWithPlayer = false;
            other.GetComponent<EnemyFight>().StopFightWithPlayer();
            OnEnemyExitsTrigger?.Invoke();
        }
    }

    private IEnumerator HitEnemy()
    {
        while (true)
        {
            if (!_player.IsHitted)
            {
                _enemy.TakeDamage(_damage);
            }
            yield return new WaitForSeconds(_delayToHit);
        }
    }
}
