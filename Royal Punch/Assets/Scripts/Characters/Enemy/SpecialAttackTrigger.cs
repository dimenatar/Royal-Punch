using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTrigger : MonoBehaviour
{
    private bool _isPlayerInTrigger;

    public bool IsPlayerInTrigger => _isPlayerInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _isPlayerInTrigger = false;
        }
    }

    private void OnDisable()
    {
        _isPlayerInTrigger = false;
    }
}
