using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private SimpleTouchController _touchController;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private PlayerFight _playerFight;
    [SerializeField] private Ragdoll _playerRagdoll;

    #region Animation triggers and bools
    private const string IDLE = "Idle";
    private const string RUN_STRAIGHT = "RunS";
    private const string RUN_LEFT = "RunL";
    private const string RUN_RIGHT = "RunR";
    private const string RUN_BACKWARDS = "RunB";

    private const string START_FIGHT = "StartFight";
    private const string END_FIGHT = "EndFight";

    private const string IS_IN_FIGHT = "IsInFight";
    private const string IS_HITTED = "IsHitted";

    private int STAND_ID = Animator.StringToHash("Idle2");
    #endregion

    private void Awake()
    {
        _playerFight.OnEnemyEntersTrigger += StartFightAnimation;
        _playerFight.OnEnemyExitsTrigger += EndStartAnimation;
        //_playerRagdoll.OnFall += () => _playerAnimator.SetBool(IS_HITTED, true);
        _playerRagdoll.OnFall += Fall;
        _playerRagdoll.OnStandedUp += EnableAnim;
        //_playerRagdoll.OnStandedUp += () => _playerAnimator.SetBool(IS_HITTED, false);
    }

    private void FixedUpdate()
    {
        SetRunningAnimation(_touchController.GetTouchPosition);
    }

    private void Start()
    {

    }

    private void Fall()
    {
        _playerAnimator.enabled = false;
    }

    private void EnableAnim() => _playerAnimator.enabled = true;

    public void StartFightAnimation() => _playerAnimator.SetTrigger(START_FIGHT);

    public void EndStartAnimation() => _playerAnimator.SetTrigger(END_FIGHT);

    private void SetRunningAnimation(Vector2 direction)
    {
        //idle
        if (direction == Vector2.zero)
        {
            _playerAnimator.SetTrigger(IDLE);
            return;
        }
        //straight
        if (direction.x >= -0.3f && direction.x <= 0.3f && direction.y > 0)
        {
            _playerAnimator.SetTrigger(RUN_STRAIGHT);
        }
        //backwards
        else if (direction.x >= -0.3f && direction.x <= 0.3f && direction.y < 0)
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
