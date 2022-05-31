using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private EnemySpecial _enemySpecial;
    [SerializeField] private EnemyFight _enemyFight;
    [SerializeField] private Character _enemy;

    #region Enemy const animations
    private const string STREAM_ATTACK = "Stream";
    private const string SPLASH_ATTACK = "Splash";
    private const string DRAGGING_ATTACK = "Dragging";
    private const string START_ATTACK = "StartFight";
    private const string END_ATTACK = "EndFight";

    private const string TO_IDLE = "FromTiredToIdle";
    private const string END_DRAGGING = "DraggingToTired";
    private const string KNOCK_ATTACK = "Knock";
    #endregion

    private bool _draggingInterrapted;

    public event Action<SpecialAttacks> OnSpecialAnimEnded;

    private void Awake()
    {
        _enemyFight.OnStartFight += () => _enemyAnimator.SetTrigger(START_ATTACK);
        _enemyFight.OnEndFight += () => _enemyAnimator.SetTrigger(END_ATTACK);
        _enemySpecial.OnSpecialAttackPicked += SetSpecialAnim;
        //_enemySpecial.OnSpecialAttackEnded += () => Invoke(nameof(TranslateFromTiredToIdle), _enemySpecial.TiredDuration);
        _enemySpecial.OnSpecialAttackEnded += TranslateFromTiredToIdle;
        _enemySpecial.OnDraggingForceStopped += DraggingInterrupted;
        _enemy.OnDied += ResetTriggers;
        OnSpecialAnimEnded += _enemySpecial.ApplySpecial;
    }

    public void ForceStop()
    {
        _enemyAnimator.Play("Empty");
    }

    public void DraggingInterrupted()
    {
        _draggingInterrapted = true;
        _enemyAnimator.Play(KNOCK_ATTACK);
    }

    public void ResetTriggers()
    {
        _enemyAnimator.ResetTrigger(STREAM_ATTACK);
        _enemyAnimator.ResetTrigger(SPLASH_ATTACK);
        _enemyAnimator.ResetTrigger(DRAGGING_ATTACK);
    }

    public void SpecialAnimEnded(SpecialAttacks attack)
    {
        print("SPECIAL ANIM ENDED");
        OnSpecialAnimEnded?.Invoke(attack);
    }

    private void SetSpecialAnim(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    _enemyAnimator.SetTrigger(STREAM_ATTACK);
                    break;
                }
            case SpecialAttacks.Dragging:
                {
                    _enemyAnimator.SetTrigger(DRAGGING_ATTACK);
                    Invoke(nameof(TranslateToTired), _enemySpecial.DraggingDuration);
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _enemyAnimator.SetTrigger(SPLASH_ATTACK);
                    break;
                }
        }
    }

    private void TranslateToTired()
    {
        if (!_draggingInterrapted)
        _enemyAnimator.SetTrigger(END_DRAGGING);
        _draggingInterrapted = false;
    }

    private void TranslateFromTiredToIdle()
    {
        print("END TIRED");
        _enemyAnimator.SetTrigger(TO_IDLE);
    }
}
