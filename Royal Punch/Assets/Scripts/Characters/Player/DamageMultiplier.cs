using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMultiplier : MonoBehaviour
{
    [SerializeField] private float _reduceDelay = 0.5f;
    [SerializeField] private float _reduceAmount = 0.1f;
    [SerializeField] private float _addDelay = 0.5f;
    [SerializeField] private float _addAmount = 0.1f;
    [SerializeField] private float _maxMultiplier = 5f;
    [SerializeField] private float _delayToReduce = 2f;

    private bool _isAdding;
    private bool _isReducing;

    private float _multiplier = 1;

    public event Action<float> OnMultiplierChanged;

    public float MaxMultiplier => _maxMultiplier;

    public float Multiplier { get => _multiplier; private set
        {
            _multiplier = value;
            OnMultiplierChanged?.Invoke(_multiplier);
        }
    }

    public void StopAdding()
    {
        if (_isAdding)
        {
            StopCoroutine(nameof(AddMultiplicity));
            _isAdding = false;
        }
        Invoke(nameof(StartReducing), _delayToReduce);
    }

    public void StartAddind()
    {
        if (_isReducing)
        {
            StopCoroutine(nameof(ReduceMultiplicity));
            _isReducing = false;
        }
        StartCoroutine(nameof(AddMultiplicity));
    }

    private void StartReducing()
    {
        //if in delay player doesn't start fight
        if (!_isAdding)
        {
            StartCoroutine(nameof(ReduceMultiplicity));
        }
    }

    private IEnumerator AddMultiplicity()
    {
        _isAdding = true;
        while (Multiplier < _maxMultiplier)
        {
            if (Multiplier == 1)
            {
                Multiplier = 1.00001f;
            }
            else
            {
                Multiplier += _addAmount;
            }
            yield return new WaitForSeconds(_addDelay);
        }
        Multiplier = _maxMultiplier;
        _isAdding = false;
    }

    private IEnumerator ReduceMultiplicity()
    {
        _isReducing = true;
        while (Multiplier > 1)
        {
            if (Multiplier - _reduceAmount > 1)
            {
                Multiplier -= _reduceAmount;
            }
            else
            {
                //ее костыли
                Multiplier = 1.00001f;
            }
            yield return new WaitForSeconds(_reduceDelay);
        }
        Multiplier = 1;
        _isReducing = false;
    }
}
