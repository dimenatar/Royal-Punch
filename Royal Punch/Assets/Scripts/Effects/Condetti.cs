using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condetti : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti1;
    [SerializeField] private ParticleSystem _confetti2;
    [SerializeField] private float _shotDuration;
    [SerializeField] private float _shotMultiplicator;
    [SerializeField] private RoundResult _roundResult;

    private void Awake()
    {
        _roundResult.OnRoundEnded += Shoot;
    }

    private void Shoot()
    {
        _confetti1.gameObject.SetActive(true);
        _confetti2.gameObject.SetActive(true);
        _confetti2.playbackSpeed = _confetti1.playbackSpeed *= _shotMultiplicator;
        Invoke(nameof(ResetToDefaultSpeed), _shotDuration);
    }

    private void ResetToDefaultSpeed()
    {
        _confetti2.playbackSpeed = _confetti1.playbackSpeed /= _shotMultiplicator;
    }
}
