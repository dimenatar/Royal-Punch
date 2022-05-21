using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecial : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Character _player;
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Timer _timerBetweenSpecialAttacks;
    [SerializeField] private Timer _specialAttackTimer;

    [SerializeField] private EnemyFight _enemyFight;

    [SerializeField] private SpecialAttackTrigger _splash;
    [SerializeField] private SpecialAttackTrigger _stream;

    [SerializeField] private float _hitPlayerForce = 2;
    [SerializeField] private float _draggingDuration = 2;
    [SerializeField] private float _draggingPlayerForce = 1;
    [SerializeField] private float _delayBetweenSpecialAttacks = 1;
    [SerializeField] private float _delayBetweenAttackPickedAndApplied = 1;
    [SerializeField] private float _specialAttackAnimationDuration = 1;

    private bool _isInSpecialAttack;

    public bool IsInSpecialAttack => _isInSpecialAttack;
    public float DelayBetweenAttackPickedAndApplied => _delayBetweenAttackPickedAndApplied;

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
        
        //_timerBetweenSpecialAttacks.StartTimer();
    }

    public void Initialise()
    {
        _timerBetweenSpecialAttacks.Initialise(_delayBetweenSpecialAttacks, startOnInit: true, repeating: true);
    }

    public void StopSpecials()
    {
        _timerBetweenSpecialAttacks.StopTimer();
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
        print(attack);
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    _isInSpecialAttack = true;
                    Invoke(nameof(DoStreamAttack), _specialAttackAnimationDuration);
                    break;
                }
            case SpecialAttacks.Dragging:
                {
                    StartCoroutine(nameof(StartDraggingPlayer));
                    _specialAttackTimer.ClearEvent();
                    _specialAttackTimer.OnTime += StopDragging;
                    _specialAttackTimer.Initialise(_draggingDuration);
                    _specialAttackTimer.StartTimer();
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _isInSpecialAttack = true;
                    Invoke(nameof(DoSplashAttack), _specialAttackAnimationDuration);
                    break;
                }
        }
    }

    private void DoSplashAttack()
    {
        if (_splash.IsPlayerInTrigger)
        {
            _player.GetSpecialHit(transform.position - _playerTransform.position, _hitPlayerForce);
        }
        OnSpecialAttackEnded?.Invoke();
    }

    private void DoStreamAttack()
    {
        if (_stream.IsPlayerInTrigger)
        {
            _player.GetSpecialHit(transform.position - _playerTransform.position, _hitPlayerForce);
        }
        OnSpecialAttackEnded?.Invoke();
    }

    private void StopDragging()
    {
        StopCoroutine(nameof(StartDraggingPlayer));
        _isInSpecialAttack = false;
        OnSpecialAttackEnded?.Invoke();
        _playerMovement.DraggingForce = Vector3.zero;
    }

    private IEnumerator StartDraggingPlayer()
    {
        while (true)
        {
            _playerMovement.DraggingForce = (transform.position - _playerTransform.position).normalized * _draggingPlayerForce;
            yield return null;
        }
    }
}
