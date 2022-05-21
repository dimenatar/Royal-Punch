using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData 
{
    private Stage _stage;
    private HealthUpgrade _healthUpgrade;
    private DamageUpgrade _damageUpgrade;

    public Stage Stage => _stage;
    public HealthUpgrade HealthUpgrade => _healthUpgrade;   
    public DamageUpgrade DamageUpgrade => _damageUpgrade;

    public UserData()
    {
        _stage = new Stage();
        _healthUpgrade = new HealthUpgrade();
        _damageUpgrade = new DamageUpgrade();
    }


    public void SaveData(Stage stage, HealthUpgrade healthUpgrade, DamageUpgrade damageUpgrade)
    {
        _stage = stage;
        _healthUpgrade = healthUpgrade;
        _damageUpgrade = damageUpgrade;
    }
}
