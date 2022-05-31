using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private UserMoney _userMoney;
    [SerializeField] private TextMeshProUGUI _values;

    private void Awake()
    {
        _userMoney.OnMoneyChanged += ChangeAmount;
    }

    private void ChangeAmount(int value)
    {
        _values.text = value.ToString();
    }
}
