using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMoney : MonoBehaviour
{
    private int _money;

    public int Money { get => _money; private set
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public delegate void MoneyChanged(int currentMoneyAmount);

    public event MoneyChanged OnMoneyChanged;

    public void Initialise(int startMoney)
    {
        Money = startMoney;
    }

    public void AddMoney(int amount) => Money += amount;

    public bool IsEnoughtMoney(int amount) => Money >= amount;

    public void ReduceMoney(int amount)
    {
        if (IsEnoughtMoney(amount))
        {
            Money -= amount;
        }
    }
}
