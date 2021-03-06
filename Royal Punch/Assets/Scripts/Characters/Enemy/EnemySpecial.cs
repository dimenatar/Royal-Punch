using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecial : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Character _player;
    [SerializeField] private Character _enemy;
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
    [SerializeField] private float _delayBetweenDraggingStopedAndKnocked = 1;

    [Header("Attack's damage")]
    [SerializeField] private int _splashDamage = 20;
    [SerializeField] private int _streamDamage = 40;
    [SerializeField] private int _knockDamage = 60;

    private bool _isInSpecialAttack;
    private bool _isDragging;
    private bool _isTired;
    public bool IsDragging => _isDragging;
    public bool IsInSpecialAttack => _isInSpecialAttack;
    public bool IsTired => _isTired;
    public float DelayBetweenAttackPickedAndApplied => _delayBetweenAttackPickedAndApplied;
    public float DraggingDuration => _draggingDuration;
    public float TiredDuration => _tiredDuration;

    public event Action <SpecialAttacks> OnSpecialAttackPicked;
    public event Action<SpecialAttacks> OnSpecialAttackEnded;
    public event Action OnSpecialPhaseEnded;
    public event Action OnDraggingForceStopped;

    private void Awake()
    {
        _timerBetweenSpecialAttacks.OnTime += PickRandomSpecialAttack;
        _timerBetweenSpecialAttacks.OnTime += () => print("ONTIME");

        OnSpecialPhaseEnded += () => _isTired = false;
        OnSpecialPhaseEnded += () => _specialAttackTimer.StartTimer();
        _enemyFight.OnStartFight += ForceStopDragging;
        OnSpecialPhaseEnded += _followPlayer.StartFollowing;
        _enemy.OnDied += Died;
    }

    public void Initialise()
    {
        _timerBetweenSpecialAttacks.Initialise(_delayBetweenSpecialAttacks, startOnInit: true, repeating: false);
    }

    public void StopSpecials()
    {
        _timerBetweenSpecialAttacks.StopTimer();
    }

    public void ApplySpecial(SpecialAttacks attack)
    {
        OnSpecialAttackEnded?.Invoke(attack);
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    DoStreamAttack();
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    DoSplashAttack();
                    break;
                }
        }
        _isTired = true;
        _isInSpecialAttack = false;
    }

    private void PickRandomSpecialAttack()
    {
        if (!_enemyFight.IsInFight && !_isInSpecialAttack && !_isTired)
        {
            // pick random attack through all of types
            //SpecialAttacks attack = SpecialAttacks.Stream;
            SpecialAttacks attack = (SpecialAttacks) UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpecialAttacks)).Length);
            StartAttack(attack);
        }
        else
        {
            _timerBetweenSpecialAttacks.StartTimer();
        }
    }

    private void StartAttack(SpecialAttacks attack)
    {
        if (!_enemyFight.IsInFight)
        {
            OnSpecialAttackPicked?.Invoke(attack);
            switch (attack)
            {
                case SpecialAttacks.Stream:
                    {
                        _isInSpecialAttack = true;
                        _followPlayer.StopFollowing();
                        break;
                    }
                case SpecialAttacks.Dragging:
                    {
                        StartCoroutine(nameof(StartDraggingPlayer));
                        _specialAttackTimer.ClearEvent();
                        _specialAttackTimer.OnTime += StopDragging;
                        _specialAttackTimer.Initialise(_draggingDuration);
                        _specialAttackTimer.StartTimer();
                        _isDragging = true;
                        break;
                    }
                case SpecialAttacks.SplashArea:
                    {
                        _isInSpecialAttack = true;
                        _followPlayer.StopFollowing();
                        //Invoke(nameof(DoSplashAttack), _specialAttackAnimationDuration);
                        break;
                    }
            }
        }
    }

    private void DoSplashAttack()
    {
        if (_splash.IsPlayerInTrigger)
        {
            print("SPLASH");
            _player.GetSpecialHit();
            _player.TakeDamage(_splashDamage);
        }
        Invoke(nameof(CallTiredEnded), _tiredDuration);
    }

    private void DoStreamAttack()
    {
        if (_stream.IsPlayerInTrigger)
        {
            _player.GetSpecialHit();
            _player.TakeDamage(_streamDamage);
        }
        Invoke(nameof(CallTiredEnded), _tiredDuration);
    }

    private void StopDragging()
    {
        if (_isDragging)
        {
            print("STOP DRAGGING");
            StopCoroutine(nameof(StartDraggingPlayer));
            Invoke(nameof(CallTiredEnded), _tiredDuration);
            _followPlayer.StopFollowing();
            OnSpecialAttackEnded?.Invoke(SpecialAttacks.Dragging);
            _playerMovement.DraggingForce = Vector3.zero;
            _isTired = true;
            _isDragging = false;
        }
    }

    private IEnumerator StartDraggingPlayer()
    {
        while (true)
        {
            _playerMovement.DraggingForce = (transform.position - _playerTransform.position).normalized * _draggingPlayerForce;
            yield return null;
        }
    }

    private void CallTiredEnded()
    {
        print("CALL TIRED ENDED");
        _timerBetweenSpecialAttacks.StartTimer();
        OnSpecialPhaseEnded?.Invoke();
    }

    //if we start fight with player
    private void ForceStopDragging()
    {
        if (_isDragging)
        {
            print("FORCE STOP DRAGGING");
            StopCoroutine(nameof(StartDraggingPlayer));
            OnDraggingForceStopped?.Invoke();
            _playerMovement.DraggingForce = Vector3.zero;
            _isDragging = false;
            Invoke(nameof(DoKnockAttack), _delayBetweenDraggingStopedAndKnocked);
        }
    }

    private void DoKnockAttack()
    {
        _player.GetSpecialHit();
        _player.TakeDamage(_knockDamage);
        Invoke(nameof(CallTiredEnded), _tiredDuration);
    }

    private void Died()
    {
        _playerMovement.DraggingForce = Vector3.zero;
        StopCoroutine(nameof(StartDraggingPlayer));
        _isInSpecialAttack = false;
        _isDragging = false;
    }
}
