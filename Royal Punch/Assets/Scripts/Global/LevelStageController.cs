using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageController : MonoBehaviour
{
    [SerializeField] private StageBundle _stages;

    private Stage _currentStage;

    public Stage CurrentStage { get => _currentStage; private set
        {
            _currentStage = value;
            OnStageChanged?.Invoke(_currentStage.StageOrder);
        }
    }

    public delegate void StageChanged(int stage);

    public event StageChanged OnStageChanged;

    public void Initialise(Stage stage)
    {
        _currentStage = stage;
    }

    public void UpgrageStage()
    {
        if (!_stages.IsLastStage(_currentStage.StageOrder))
        {
            _currentStage = _stages[_currentStage.StageOrder];
        }
    }
}
