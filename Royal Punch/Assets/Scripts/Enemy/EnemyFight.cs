using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    [SerializeField] private int _baseAttackDamage = 1;
    [SerializeField] private float _delayBetweenHits = 0.5f;
    [SerializeField] private Character _player;
    [SerializeField] private EnemySpecial _enemySpecial;

    private bool _isInFight;

    public bool IsInFight => _isInFight;

    public event Action OnStartFight;
    public event Action OnEndFight;

    private void OnTriggerStay(Collider other)
    {
        //in case some troubles
        if (!_isInFight)
        {
            if (other.GetComponent<Character>())
            {
                StartFightWithPlayer();
            }
        }
    }

    public void StopFightWithPlayer()
    {
        if (_isInFight)
        {
            OnEndFight?.Invoke();
            StopCoroutine(nameof(AttackPlayer));
            _isInFight = false;
        }
    }

    public void StartFightWithPlayer()
    {
        if (!_enemySpecial.IsInSpecialAttack)
        {
            OnStartFight?.Invoke();
            StartCoroutine(nameof(AttackPlayer));
            _isInFight = true;
        }
    }

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            _player.TakeDamage(_baseAttackDamage);
            yield return new WaitForSeconds(_delayBetweenHits);
        }
    }
}
