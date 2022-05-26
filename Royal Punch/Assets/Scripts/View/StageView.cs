using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageView : MonoBehaviour
{
    [SerializeField] private LevelStageController _levelStageController;
    [SerializeField] private TextMeshProUGUI _value;

    private void DisplayStage(int stage)
    {
        _value.text = stage.ToString();
    }
}
