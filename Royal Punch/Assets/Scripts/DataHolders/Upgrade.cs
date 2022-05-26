using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    [SerializeField] private int _order;
    [SerializeField] private int _priceToNext;

    public int Order => _order;
    public int PriceToNext => _priceToNext;

    public void SetOrder(int order)
    {
        _order = order;
    }
}
