using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private EnemySpecial _enemySpecial;
    [SerializeField] private EnemyFight _enemyFight;

    #region Enemy const animations
    private const string STREAM_ATTACK = "Stream";
    private const string SPLASH_ATTACK = "Splash";
    private const string DRAGGING_ATTACK = "Dragging";
    private const string START_ATTACK = "StartFight";
    private const string END_ATTACK = "EndFight";
    #endregion

    private void Awake()
    {
        _enemyFight.OnStartFight += () => _enemyAnimator.SetTrigger(START_ATTACK);
        _enemyFight.OnEndFight += () => _enemyAnimator.SetTrigger(END_ATTACK);
        _enemySpecial.OnSpecialAttackPicked += SetSpecialAnim;
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
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _enemyAnimator.SetTrigger(SPLASH_ATTACK);
                    break;
                }
        }
    }
}
