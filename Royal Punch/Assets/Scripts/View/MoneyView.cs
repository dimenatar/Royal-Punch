using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private UserMoney _userMoney;
    [SerializeField] private TextMeshProUGUI _values;

    [SerializeField] private float _scaleAnimationDuration;
    [SerializeField] private float _unscaleAnimationDuration;
    [SerializeField] private float _animatedScaleMultiplicator;

    private Vector3 _startScale;
    private Vector3 _animatedScale;

    private void Awake()
    {
        _userMoney.OnMoneyChanged += ChangeAmount;
    }

    private void Start()
    {
        _startScale = _values.transform.lossyScale;
        _animatedScale = _startScale * _animatedScaleMultiplicator;
    }

    private void ChangeAmount(int value)
    {
        _values.text = value.ToString();
        _values.transform.DOScale(_animatedScale, _scaleAnimationDuration).OnComplete(() => _values.transform.DOScale(_startScale, _unscaleAnimationDuration));
    }
}
