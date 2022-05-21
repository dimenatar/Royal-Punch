using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgradeManager : MonoBehaviour
{
    private DamageUpgrade _currentUpgrade;

    public DamageUpgrade CurrentUpgrade { get => _currentUpgrade; set => _currentUpgrade = value; }

    public void Initialise(DamageUpgrade damageUpgrade)
    {
        _currentUpgrade = damageUpgrade;
    }
}
