using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecial : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Timer _timerBetweenSpecialAttacks;
    [SerializeField] private Timer _specialAttackTimer;

    [SerializeField] private EnemyFight _enemyFight;

    [SerializeField] private float _draggingPlayerForce = 1;
    [SerializeField] private float _delayBetweenSpecialAttacks = 1;

    private bool _isInSpecialAttack;

    public bool IsInSpecialAttack => _isInSpecialAttack;

    public delegate void SpecialAttackPicked(SpecialAttacks attack);

    public event SpecialAttackPicked OnSpecialAttackPicked;
    public event Action OnSpecialAttackEnded;

    private void Awake()
    {
        _timerBetweenSpecialAttacks.OnTime += PickRandomSpecialAttack;
        OnSpecialAttackPicked += StartAttack;
    }

    private void Start()
    {
        _timerBetweenSpecialAttacks.Initialise(_delayBetweenSpecialAttacks);
    }

    private void PickRandomSpecialAttack()
    {
        if (!_enemyFight.IsInFight)
        {
            _isInSpecialAttack = true;
            // pick random attack through all of types
            SpecialAttacks attack = (SpecialAttacks)UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpecialAttacks)).Length);
            OnSpecialAttackPicked?.Invoke(attack);
        }
        _timerBetweenSpecialAttacks.UpdateTimer();
    }

    private void StartAttack(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    break;
                }
            case SpecialAttacks.Dragging:
                {
                    StartCoroutine(nameof(StartDraggingPlayer));
                    _specialAttackTimer.ClearEvent();
                    _specialAttackTimer.OnTime += () => StopCoroutine(nameof(StartDraggingPlayer));
                    _specialAttackTimer.OnTime += () => _isInSpecialAttack = false;
                    _specialAttackTimer.OnTime += () => OnSpecialAttackEnded?.Invoke();
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    break;
                }
        }
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
