using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData 
{
    private Stage _stage;
    private HealthUpgrade _healthUpgrade;
    private DamageUpgrade _damageUpgrade;
    private int _money = 500;

    public Stage Stage => _stage;
    public HealthUpgrade HealthUpgrade => _healthUpgrade;   
    public DamageUpgrade DamageUpgrade => _damageUpgrade;
    public int Money => _money;

    public UserData()
    {
        _stage = new Stage();
        _healthUpgrade = new HealthUpgrade();
        _damageUpgrade = new DamageUpgrade();
    }


    public void SaveData(Stage stage, HealthUpgrade healthUpgrade, DamageUpgrade damageUpgrade, int money)
    {
        _stage = stage;
        _healthUpgrade = healthUpgrade;
        _damageUpgrade = damageUpgrade;
        _money = money;
    }
}
