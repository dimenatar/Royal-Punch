using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    [SerializeField] private Character _player;
    [SerializeField] private Character _enemy;

    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private List<GameObject> _UIElementsToShow;
    [SerializeField] private ShopView _shopView;

    [SerializeField] private Transform _playerStartPoint;

    [SerializeField] private PlayerFight _playerFight;

    [SerializeField] private EndPanel _endPanel;
    [SerializeField] private Animator _camera;

    public void ResetLevel(bool win)
    {
        _camera.SetTrigger("Out");
        if (!win)
        {
            _player.RestoreRagdoll();
        }
        else
        {
            _enemy.RestoreRagdoll();
        }
        //_playerAnimator.Play("Idle");
        //_playerFight.ForceReset();
        _playerTransform.position = _playerStartPoint.position;

    }
}
