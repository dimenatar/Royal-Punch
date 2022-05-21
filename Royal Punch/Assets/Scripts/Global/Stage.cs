using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    [SerializeField] private int _stageOrder = 1;
    [SerializeField] private int _enemyHealth = 120;

    public int StageOrder => _stageOrder;
    public int EnemyHealth => _enemyHealth;

    public void SetOrder(int order)
    {
        _stageOrder = order;
    }
}
