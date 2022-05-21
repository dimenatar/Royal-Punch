using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private LevelStageController _stageController;
    [SerializeField] private HealthUpgradeManager _healthManager;
    [SerializeField] private DamageUpgradeManager _damageManager;

    private UserData _userData;

    public UserData UserData => _userData;

    private void Awake()
    {
        LoadData();
        InitialiseControllers();
    }

    private void LoadData()
    {
        _userData = UserProgressManager.LoadUserData();
        if (_userData == null)
        {
            _userData = new UserData();
            UserProgressManager.SaveUserData(_userData);
        }
    }

    private void InitialiseControllers()
    {
        _stageController.Initialise(_userData.Stage);
        _healthManager.Initialise(_userData.HealthUpgrade);
        _damageManager.Initialise(_userData.DamageUpgrade);
    }
}
