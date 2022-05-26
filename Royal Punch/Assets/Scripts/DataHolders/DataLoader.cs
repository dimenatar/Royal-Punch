using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private LevelStageController _stageController;
    [SerializeField] private HealthUpgradeManager _healthManager;
    [SerializeField] private DamageUpgradeManager _damageManager;
    [SerializeField] private UserMoney _userMoney;
    [SerializeField] private UpgradeBundle _upgradeBundle;
    [SerializeField] private StageBundle _stageBundle;

    private UserData _userData;

    public UserData UserData => _userData;

    private void Awake()
    {
        LoadData();
        
        Application.quitting += SaveData;
    }

    private void Start()
    {
        InitialiseControllers();
    }

    private void LoadData()
    {
        _userData = UserProgressManager.LoadUserData();
        if (_userData == null)
        {
            _userData = new UserData();
            _userData.SaveData(_stageBundle[0], _upgradeBundle.HealthUpgrades[0], _upgradeBundle.DamageUpgrades[0], 100);
            UserProgressManager.SaveUserData(_userData);
        }
    }

    private void InitialiseControllers()
    {
        _stageController.Initialise(_userData.Stage);
        _healthManager.Initialise(_userData.HealthUpgrade);
        _damageManager.Initialise(_userData.DamageUpgrade);
        print(_userData.Money);
        _userMoney.Initialise(_userData.Money);
    }

    private void SaveData()
    {
        _userData.SaveData(_stageController.CurrentStage, _healthManager.HealthUpgrade, _damageManager.CurrentUpgrade, _userMoney.Money);
        UserProgressManager.SaveUserData(UserData);
    }
}
