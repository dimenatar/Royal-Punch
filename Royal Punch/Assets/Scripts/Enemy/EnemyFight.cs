using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    [SerializeField] private int _baseAttackDamage = 1;
    [SerializeField] private float _delayBetweenHits = 0.5f;
    [SerializeField] private Character _player;

    private bool _isInFight;

    public bool IsInFight => _isInFight;

    public void StopFightWithPlayer()
    {
        StopCoroutine(nameof(AttackPlayer));
        _isInFight = false;
    }

    public void StartFightWithPlayer()
    {
        StartCoroutine(nameof(AttackPlayer));
        _isInFight = true;
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
