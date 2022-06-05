using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorByDamageMultiplier : MonoBehaviour
{
    [SerializeField] private DamageMultiplier _damageMultiplier;
    [SerializeField] private TextMeshPro _multiplierText;
    [SerializeField] private Gradient _gradient;

    private Color _currentColor;

    public Color CurrentColor => _currentColor;

    private void Awake()
    {
        _damageMultiplier.OnMultiplierChanged += UpdateColor;
    }

    private void Start()
    {
        _currentColor = _gradient.Evaluate(0);
    }

    private void UpdateColor(float currentMultiplier)
    {
        _currentColor = _gradient.Evaluate((currentMultiplier - 1)/(_damageMultiplier.MaxMultiplier - 1));
        _multiplierText.color = _currentColor;
    }
}
