using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage Bundle", menuName = "Stage Bundle", order = 22)]
public class StageBundle : ScriptableObject
{
    [SerializeField] private List<Stage> _stages;

    public Stage this [int index]
    {
        get { return _stages[index]; }
    }

    public int Count => _stages.Count;
}
