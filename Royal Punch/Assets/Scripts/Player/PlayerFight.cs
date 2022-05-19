using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public event Action OnEnemyEntersTrigger;
    public event Action OnEnemyExitsTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyFight>())
        {
            other.GetComponent<EnemyFight>().IsInTriggerWithPlayer = true;
            other.GetComponent<EnemyFight>().StartFightWithPlayer();
            OnEnemyEntersTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyFight>())
        {
            other.GetComponent<EnemyFight>().IsInTriggerWithPlayer = false;
            other.GetComponent<EnemyFight>().StopFightWithPlayer();
            OnEnemyExitsTrigger?.Invoke();
        }
    }
}
