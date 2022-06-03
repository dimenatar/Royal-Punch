using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _streamSmoke;
    [SerializeField] private ParticleSystem _splashSmoke;
    [SerializeField] private EnemyAnimations _enemyAnimations;
    [SerializeField] private EnemySpecial _enemySpecial;

    [SerializeField] private GameObject _mainSparks;
    [SerializeField] private GameObject _handSparks;

    private void Awake()
    {
        //_enemyAnimations.OnSpecialAnimEnded += DisplaySpecialEffect;
        _enemySpecial.OnSpecialAttackEnded += DisplaySpecialEffect;
        _enemySpecial.OnSpecialAttackPicked += DisplaySparks;
        _enemySpecial.OnDraggingForceStopped += () => DisplaySpecialEffect(SpecialAttacks.Dragging);
    }

    private void DisplaySpecialEffect(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    DisplayStreamSmoke();
                    _mainSparks.SetActive(false);
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    DisplaySplashSmoke();
                    _mainSparks.SetActive(false);
                    break;
                }
            case SpecialAttacks.Dragging:
                {
                    _handSparks.SetActive(false);
                    break;
                }
        }
    }


    private void DisplayStreamSmoke()
    {
        _streamSmoke.Play();
    }

    private void DisplaySplashSmoke()
    {
        _splashSmoke.Play();
    }

    private void DisplaySparks(SpecialAttacks attack)
    {
        
        if (attack == SpecialAttacks.Stream || attack == SpecialAttacks.SplashArea)
        {
            _mainSparks.SetActive(true);
        }
        else if (attack == SpecialAttacks.Dragging)
        {
            _handSparks.SetActive(true);
        }
    }
}
