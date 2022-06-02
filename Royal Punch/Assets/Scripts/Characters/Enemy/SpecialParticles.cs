using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _streamSmoke;
    [SerializeField] private ParticleSystem _splashSmoke;
    [SerializeField] private EnemyAnimations _enemyAnimations;

    private void Awake()
    {
        _enemyAnimations.OnSpecialAnimEnded += DisplaySpecialEffect;
    }

    private void DisplaySpecialEffect(SpecialAttacks attack)
    {
        switch (attack)
        {
            case SpecialAttacks.Stream:
                {
                    DisplayStreamSmoke();
                    break;
                }
            case SpecialAttacks.SplashArea:
                {
                    DisplaySplashSmoke();
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
}
