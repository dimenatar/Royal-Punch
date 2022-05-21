using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    [SerializeField] private int _order;
    [SerializeField] private int _price;

    public int Order => _order;
    public int Price => _price;

    public void SetOrder(int order)
    {
        _order = order;
    }
}
