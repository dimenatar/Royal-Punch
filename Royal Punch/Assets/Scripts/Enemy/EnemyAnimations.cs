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

    #endregion

    private void Awake()
    {
        _enemyFight.OnStartFight += () => _enemyAnimator.SetTrigger(START_ATTACK);
        _enemyFight.OnEndFight += () => _enemyAnimator.SetTrigger(END_ATTACK);
        _enemySpecial.OnSpecialAttackPicked += SetSpecialAnim;
        //_enemySpecial.OnSpecialAttackEnded += () => Invoke(nameof(TranslateFromTiredToIdle), _enemySpecial.TiredDuration);
        _enemySpecial.OnSpecialAttackEnded += TranslateFromTiredToIdle;
        _enemySpecial.OnDraggingForceStopped += ForceStopSpecial;
        _enemy.OnDied += ResetTriggers;
    }

    public void ForceStop()
    {
        _enemyAnimator.Play("Empty");
    }

    public void ForceStopSpecial()
    {
        print("FORCE STOP SPECIAL");
        _enemyAnimator.Play("Idle", 1);
    }

    public void ResetTriggers()
    {
        _enemyAnimator.ResetTrigger(STREAM_ATTACK);
        _enemyAnimator.ResetTrigger(SPLASH_ATTACK);
        _enemyAnimator.ResetTrigger(DRAGGING_ATTACK);
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
        _enemyAnimator.SetTrigger(END_DRAGGING);
    }

    private void TranslateFromTiredToIdle()
    {
        _enemyAnimator.SetTrigger(TO_IDLE);
    }
}
