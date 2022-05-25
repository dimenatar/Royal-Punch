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

    public bool IsInTriggerWithPlayer { get; set; }
    public bool IsInFight => _isInFight;

    public event Action OnStartFight;
    public event Action OnEndFight;

    private void Awake()
    {
        _enemySpecial.OnSpecialAttackEnded += TryStartFightWithPlayer;
    }

    private void OnTriggerStay(Collider other)
    {
        //in case some troubles
        //if (!_isInFight)
        //{
        //    if (other.GetComponent<Character>())
        //    {
        //        print("TRIGGER START");
        //        StartFightWithPlayer();
        //    }
        //}
    }

    public void StopFightWithPlayer()
    {
        if (_isInFight)
        {
            _isInFight = false;
            OnEndFight?.Invoke();
            StopCoroutine(nameof(AttackPlayer));
        }
    }

    public void StartFightWithPlayer()
    {
        if (!_enemySpecial.IsInSpecialAttack && !_enemySpecial.IsDragging)
        {
            _isInFight = true;
            OnStartFight?.Invoke();
            StartCoroutine(nameof(AttackPlayer));
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

    private void TryStartFightWithPlayer()
    {
        if (IsInTriggerWithPlayer && !_isInFight && !_enemySpecial.IsDragging)
        {
            StartFightWithPlayer();
        }
    }
}
