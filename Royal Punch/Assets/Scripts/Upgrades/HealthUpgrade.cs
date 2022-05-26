using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthUpgrade : Upgrade
{
    [SerializeField] private int _health = 100;

    public int Health => _health;
}
