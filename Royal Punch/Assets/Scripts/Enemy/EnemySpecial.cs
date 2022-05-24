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
    [SerializeField] private FollowPlayer _followPlayer;

    [SerializeField] private SpecialAttackTrigger _splash;
    [SerializeField] private SpecialAttackTrigger _stream;

    [SerializeField] private float _hitPlayerForce = 2;
    [SerializeField] private float _draggingDuration = 2;
    [SerializeField] private float _draggingPlayerForce = 1;
    [SerializeField] private float _delayBetweenSpecialAttacks = 10;
    [SerializeField] private float _delayBetweenAttackPickedAndApplied = 2;
    [SerializeField] private float _specialAttackAnimationDuration = 2;
    [SerializeField] private float _tiredDuration = 5;

    [Header("Attack's damage")]
    [SerializeField] private int _streamDamage = 40;
    [SerializeField] private int _splashDamage = 20;

    private bool _isInSpecialAttack;
    private bool _isDragging;

    public bool IsInSpecialAttack => _isInSpecialAttack;
    public float DelayBetweenAttackPickedAndApplied => _delayBetweenAttackPickedAndApplied;
    public float DraggingDuration => _draggingDuration;
    public float TiredDuration => _tiredDuration;

    public delegate void SpecialAttackPicked(SpecialAttacks attack);

    public event SpecialAttackPicked OnSpecialAttackPicked;
    public event Action OnSpecialAttackEnded;
    public event Action OnDraggingForceStopped;

    private void Awake()
    {
        _timerBetweenSpecialAttacks.OnTime += PickRandomSpecialAttack;
        OnSpecialAttackPicked += StartAttack;
        OnSpecialAttackEnded += () => _timerBetweenSpecialAttacks.StartTimer();
        OnSpecialAttackEnded += () => _isInSpecialAttack = false;
        _enemyFight.OnStartFight += ForceStopDragging;
        OnSpecialAttackPicked += (attack) =>  _followPlayer.StopFollowing();
        OnSpecialAttackEnded += _followPlayer.StartFollowing;
    }

    private void Start()
    {
        
        //_timerBetweenSpecialAttacks.StartTimer();
    }

    public void Initialise()
    {
        _timerBetweenSpecialAttacks.Initialise(_delayBetweenSpecialAttacks, startOnInit: true, repeating: false);
    }

    public void StopSpecials()
    {
        _timerBetweenSpecialAttacks.StopTimer();
    }

    private void PickRandomSpecialAttack()
    {
        if (!_enemyFight.IsInFight)
        {
            // pick random attack through all of types
            SpecialAttacks attack = (SpecialAttacks) UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpecialAttacks)).Length);
            OnSpecialAttackPicked?.Invoke(attack);
        }
        else
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
            print("SPLASH");
            _player.GetSpecialHit(_hitPlayerForce);
            _player.TakeDamage(_splashDamage);

        }
        Invoke(nameof(CallSpecialEnded), _tiredDuration);
    }

    private void DoStreamAttack()
    {
        if (_stream.IsPlayerInTrigger)
        {
            _player.GetSpecialHit(_hitPlayerForce);
            _player.TakeDamage(_streamDamage);
        }
        Invoke(nameof(CallSpecialEnded), _tiredDuration);
    }

    private void StopDragging()
    {
        StopCoroutine(nameof(StartDraggingPlayer));
        Invoke(nameof(CallSpecialEnded), _tiredDuration);
        _playerMovement.DraggingForce = Vector3.zero;
        _isInSpecialAttack = true;
        _isDragging = false;
    }

    private IEnumerator StartDraggingPlayer()
    {
        while (true)
        {
            _playerMovement.DraggingForce = (transform.position - _playerTransform.position).normalized * _draggingPlayerForce;
            yield return null;
        }
    }

    private void CallSpecialEnded()
    {
        OnSpecialAttackEnded?.Invoke();
    }

    //if we start fight with player
    private void ForceStopDragging()
    {
        print("FORCE STOP DRAGGING");
        if (_isDragging)
        {
            StopCoroutine(nameof(StartDraggingPlayer));
            OnDraggingForceStopped?.Invoke();
            _playerMovement.DraggingForce = Vector3.zero;
            _isDragging = false;
            _timerBetweenSpecialAttacks.StartTimer();
        }
    }
}
