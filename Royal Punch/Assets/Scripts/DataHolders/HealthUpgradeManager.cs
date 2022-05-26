using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthUpgradeManager : MonoBehaviour
{
    private HealthUpgrade _healthUpgrade;

    public HealthUpgrade HealthUpgrade { get => _healthUpgrade; set => _healthUpgrade = value; }

    public event Action OnInitialised;

    public void Initialise(HealthUpgrade healthUpgrade)
    {
        print("Init health");
        _healthUpgrade = healthUpgrade;
        OnInitialised?.Invoke();
    }
}
