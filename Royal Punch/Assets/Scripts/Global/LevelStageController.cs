using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageController : MonoBehaviour
{
    [SerializeField] private StageBundle _stages;

    private Stage _currentStage;

    public Stage CurrentStage {
        get
        {
            if (_currentStage != null)
                return _currentStage;
            else return _stages[0];
        }
        private set
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
            CurrentStage = _stages[_currentStage.StageOrder];
        }
    }
}
