using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //LoadData();
        SceneManager.sceneLoaded += (s, e) => LoadData();
        Application.quitting += ExitFromGame;
    }

    private void Start()
    {
        InitialiseControllers();
    }

    public void SaveData()
    {
        _userData.SaveData(_stageController.CurrentStage, _healthManager.HealthUpgrade, _damageManager.CurrentUpgrade, _userMoney.Money);
    }

    private void LoadData()
    {
        _userData = UserProgressManager.UserData;
        if (_userData != null) return;

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
        _userMoney.Initialise(_userData.Money);
        _stageController.Initialise(_userData.Stage);
        _healthManager.Initialise(_userData.HealthUpgrade);
        _damageManager.Initialise(_userData.DamageUpgrade);
    }

    private void ExitFromGame()
    {
        _userData.SaveData(_stageController.CurrentStage, _healthManager.HealthUpgrade, _damageManager.CurrentUpgrade, _userMoney.Money);
        UserProgressManager.SaveUserData(UserData);
    }
}
