using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageMultiplierView : MonoBehaviour
{
    [SerializeField] private DamageMultiplier _damageMultiplier;
    [SerializeField] private GameObject _damageView;
    [SerializeField] private TextMeshPro _damageText;

    [SerializeField] private Vector3 _animatedScale;
    [SerializeField] private float _animationDuration;

    private bool _shouldShow = true;

    private void Awake()
    {
        _damageMultiplier.OnMultiplierChanged += UpdateValue;
    }

    public void CompletlyHide()
    {
        _shouldShow = false;
        _damageView.SetActive(false);
    }

    private void UpdateValue(float damage)
    {
        if (damage > 1)
        {
            if (!_damageView.activeSelf && _shouldShow)
            {
                _damageView.SetActive(true);
            }
        }
        else
        {
            _damageView.SetActive(false);
        }
        _damageText.transform.DOPunchScale(_animatedScale, _animationDuration, 0, 1);
        _damageText.text = $"X {damage:F1}";

    }
}
