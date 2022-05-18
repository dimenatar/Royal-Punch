using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private SimpleTouchController _touchController;
    [SerializeField] private Animator _playerAnimator;

    #region Animation triggers and bools
    private const string IDLE = "Idle";
    private const string RUN_STRAIGHT = "RunS";
    private const string RUN_LEFT = "RunL";
    private const string RUN_RIGHT = "RunR";
    private const string RUN_BACKWARDS = "RunB";

    private const string IS_RUNNING = "IsRunning";
    private const string IS_IN_FIGHT = "IsInFight";
    private const string IS_HITTED = "IsHitted";
    #endregion

    private void FixedUpdate()
    {
        SetRunningAnimation(_touchController.GetTouchPosition);
    }

    public void GetHit()
    {

    }

    private void SetRunningAnimation(Vector2 direction)
    {
        //idle
        if (direction == Vector2.zero)
        {
            _playerAnimator.SetTrigger(IDLE);
            return;
        }
        //straight
        if (direction.x >= -0.1f && direction.x <= 0.1f && direction.y > 0)
        {
            _playerAnimator.SetTrigger(RUN_STRAIGHT);
        }
        //backwards
        else if (direction.x >= -0.1f && direction.x <= 0.1f && direction.y < 0)
        {
            _playerAnimator.SetTrigger(RUN_BACKWARDS);
        }
        //left
        else if (direction.x < 0)
        {
            _playerAnimator.SetTrigger(RUN_LEFT);
        }
        //right
        else
        {
            _playerAnimator.SetTrigger(RUN_RIGHT);
        }
    }
}
