using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Upgrade Bundle", menuName = "Upgrade Bundle", order = 21)]
public class UpgradeBundle : ScriptableObject
{
    [SerializeField] private List<DamageUpgrade> _damageUpgrades;
    [SerializeField] private List<HealthUpgrade> _healthUpgrades;

    public List<DamageUpgrade> DamageUpgrades => _damageUpgrades;
    public List<HealthUpgrade> HealthUpgrades => _healthUpgrades;
}
