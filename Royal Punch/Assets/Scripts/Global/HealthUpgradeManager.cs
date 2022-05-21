using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgradeManager : MonoBehaviour
{
    private HealthUpgrade _healthUpgrade;

    public HealthUpgrade HealthUpgrade { get => _healthUpgrade; set => _healthUpgrade = value; }

    public void Initialise(HealthUpgrade healthUpgrade)
    {
        _healthUpgrade = healthUpgrade;
    }
}
