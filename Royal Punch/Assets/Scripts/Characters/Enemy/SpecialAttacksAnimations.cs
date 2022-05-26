using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttacksAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemySpecial _special;

    private const string STREAM_IN = "Stream in";
    private const string STREAM_OUT = "Stream out";
    private const string SPLASH_IN = "Circle in";
    private const string SPLASH_OUT = "Circle out";
    private const string DRAGGING = "Dragging";

    private string _outAnimation = "";

    private void Awake()
    {
        _special.OnSpecialAttackPicked += StartAnimation;
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
                    _outAnimation = STREAM_OUT;
                    Invoke(nameof(AnimateOut), _special.DelayBetweenAttackPickedAndApplied);
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    _animator.SetTrigger(SPLASH_IN);
                    _outAnimation = SPLASH_OUT;
                    Invoke(nameof(AnimateOut), _special.DelayBetweenAttackPickedAndApplied);
                    break;
                }
        }

    }

    private void AnimateOut()
    {
        _animator.SetTrigger(_outAnimation);
    }
}
