using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStarter : MonoBehaviour
{
    [SerializeField] private float _delayToEnableEnemy = 0.5f;

    [SerializeField] private Animator _camera;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private Animator _specialsAnimator;

    [SerializeField] private EnemySpecial _enemySpecial;

    [SerializeField] private List<GameObject> _UIElementsToHide;
    [SerializeField] private List<GameObject> _UIElementsToShow;

    [SerializeField] private HealthUpgradeManager _healthUpgradeManager;
    [SerializeField] private DamageUpgradeManager _damageUpgradeManager;
    [SerializeField] private LevelStageController _levelStageController;

    [SerializeField] private PlayerFight _playerFight;
    [SerializeField] private Character _player;
    [SerializeField] private Character _enemy;

    [SerializeField] private TouchPosition _touchPosition;
    [SerializeField] private GameObject _touchable;

    [SerializeField] private Ragdoll _playerRagdoll;
    [SerializeField] private Ragdoll _enemyRagdoll;

    [SerializeField] private GameObject _enemyObject;

    public event Action OnCameraMoved;

    private void Awake()
    {
        OnCameraMoved += StartRound;
        
    }

    private void Start()
    {
       // RotateCamera();
    }
        

    public void RotateCamera()
    {
        _touchable.SetActive(false);
        _camera.SetTrigger("In");
        _UIElementsToHide.ForEach(element => element.SetActive(false));
        Invoke(nameof(CameraRotated), 1);
        Invoke(nameof(EnableEnemy), _delayToEnableEnemy);
        print(_levelStageController.CurrentStage.EnemyHealth);
        
    }

    public void CameraRotated() => OnCameraMoved?.Invoke();

    public void StartRound()
    { 
        _player.Initialise(_healthUpgradeManager.HealthUpgrade.Health);

        _playerAnimator.SetTrigger("Start");
        _playerFight.Initialise(_damageUpgradeManager.CurrentUpgrade.Damage);

        _touchPosition.Enabletouch();
        _enemySpecial.Initialise();

        _playerRagdoll.Initialise();
        _enemyRagdoll.Initialise();

        _specialsAnimator.enabled = true;

        _UIElementsToShow.ForEach(element => element.SetActive(true));
    }

    private void EnableEnemy()
    {
        _enemyObject.SetActive(true);
        _enemy.Initialise(_levelStageController.CurrentStage.EnemyHealth);
    }
}
