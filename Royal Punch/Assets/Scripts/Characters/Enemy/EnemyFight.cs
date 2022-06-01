using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    [SerializeField] private int _baseAttackDamage = 1;
    [SerializeField] private float _delayBetweenHits = 0.5f;
    [SerializeField] private Character _player;
    [SerializeField] private Character _enemy;
    [SerializeField] private EnemySpecial _enemySpecial;

    [Header("Angle to determinate start fight with player")]
    [SerializeField] private float _borderAngle = 60;

    public bool _isInFight;

    public bool IsInTriggerWithPlayer { get; set; }
    public bool IsInFight => _isInFight;
    public float BorderAngle => _borderAngle;

    public event Action OnStartFight;
    public event Action OnEndFight;

    private void Awake()
    {
        _enemySpecial.OnSpecialAttackEnded += TryStartFightWithPlayer;
        _enemy.OnDied += StopFightWithPlayer;
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

    private void FixedUpdate()
    {
        if (IsInTriggerWithPlayer)
        {
            if (AngleRangeCheck(_enemy.transform, _player.transform, _borderAngle))
            {
                StartFightWithPlayer();
            }
            else
            {
                StopFightWithPlayer();
            }
        }
    }

    public static bool AngleRangeCheck(Transform point1, Transform point2, float borderAngle)
    {
        var relativePos = point2.position - point1.position;
        var angle = Vector3.Angle(relativePos, point1.forward);
        return angle <= borderAngle;
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
        if (!_enemySpecial.IsInSpecialAttack && !_isInFight && !_enemySpecial.IsTired)
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
