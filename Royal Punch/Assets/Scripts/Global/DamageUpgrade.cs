using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageUpgrade : Upgrade
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}
