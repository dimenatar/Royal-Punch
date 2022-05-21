using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSetter : MonoBehaviour
{
    [SerializeField] private UpgradeBundle _upgrades;
    [SerializeField] private StageBundle _stages;

    private void Start()
    {
        for (int i = 0; i < _upgrades.DamageUpgrades.Count; i++)
        {
            _upgrades.DamageUpgrades[i].SetOrder(i + 1);
            _upgrades.HealthUpgrades[i].SetOrder(i + 1);
        }

        for (int i = 0; i < _stages.Count; i++)
        {
            _stages[i].SetOrder(i + 1);
        }
    }
}
