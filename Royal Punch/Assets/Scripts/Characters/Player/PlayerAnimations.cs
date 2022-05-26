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

    private readonly int SPEED = Animator.StringToHash("Speed");
    private readonly int X_DIRECTION = Animator.StringToHash("X");
    private readonly int Z_DIRECTION = Animator.StringToHash("Z");

    private int STAND_ID = Animator.StringToHash("Idle2");
    #endregion

    private float _animationSpeed;

    private void Awake()
    {
        _playerFight.OnEnemyEntersTrigger += StartFightAnimation;
        _playerFight.OnEnemyExitsTrigger += EndFightAnimation;
        //_playerRagdoll.OnFall += () => _playerAnimator.SetBool(IS_HITTED, true);
        _playerRagdoll.OnFall += Fall;
        _playerRagdoll.OnStandedUp += EnableAnim;
        //_playerRagdoll.OnStandedUp += () => _playerAnimator.SetBool(IS_HITTED, false);
    }

    private void Update()
    {
        var dir = _touchController.GetTouchPosition;
        _animationSpeed = Mathf.Abs(dir.x / 2) + Mathf.Abs(dir.y / 2);
        _playerAnimator.SetFloat(SPEED, _animationSpeed);
        _playerAnimator.SetFloat(X_DIRECTION, dir.x);
        _playerAnimator.SetFloat(Z_DIRECTION, dir.y);
    }

    private void Fall()
    {
        _playerAnimator.enabled = false;
    }

    private void EnableAnim() => _playerAnimator.enabled = true;

    public void GoToMainIdle()
    {
        _playerAnimator.SetTrigger("Idle1");
    }

    public void Win()
    {
        _playerAnimator.SetTrigger("Win");
    }

    public void StartFightAnimation() => _playerAnimator.SetTrigger(START_FIGHT);

    public void EndFightAnimation() => _playerAnimator.SetTrigger(END_FIGHT);

    private void SetRunningAnimation(Vector2 direction)
    {
        ////idle
        //if (direction == Vector2.zero)
        //{
        //    _playerAnimator.SetTrigger(IDLE);
        //    return;
        //}
        ////straight
        //if (direction.x >= -0.3f && direction.x <= 0.3f && direction.y > 0)
        //{
        //    _playerAnimator.SetTrigger(RUN_STRAIGHT);
        //}
        ////backwards
        //else if (direction.x >= -0.3f && direction.x <= 0.3f && direction.y < 0)
        //{
        //    _playerAnimator.SetTrigger(RUN_BACKWARDS);
        //}
        ////left
        //else if (direction.x < 0)
        //{
        //    _playerAnimator.SetTrigger(RUN_LEFT);
        //}
        ////right
        //else
        //{
        //    _playerAnimator.SetTrigger(RUN_RIGHT);
        //}
    }
}
