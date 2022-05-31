using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttacksAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemySpecial _special;
    [SerializeField] private EnemyAnimations _enemyAnimations;

    private const string STREAM_IN = "Stream in";
    private const string STREAM_OUT = "Stream out";
    private const string SPLASH_IN = "Circle in";
    private const string SPLASH_OUT = "Circle out";
    private const string DRAGGING = "Dragging";

    private void Awake()
    {
        _special.OnSpecialAttackPicked += StartAnimation;
        _enemyAnimations.OnSpecialAnimEnded += AnimateOut;
    }

    private void StartAnimation(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Dragging:
                {
                    break;
                }
            case SpecialAttacks.Stream:
                {
                    _animator.SetTrigger(STREAM_IN);
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _animator.SetTrigger(SPLASH_IN);
                    break;
                }
        }

    }

    private void AnimateOut(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    _animator.SetTrigger(STREAM_OUT);
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _animator.SetTrigger(SPLASH_OUT);
                    break;
                }
            default: return;
        }

    }
}
