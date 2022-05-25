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

    [SerializeField] private Animator _camera;

    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private GameObject _touchable;

    [SerializeField] private GameObject _playerHealthBar;

    public void ResetLevel(bool win)
    {
        print("RESET");
      
        _playerAnimations.GoToMainIdle();
        _camera.SetTrigger("Out");
        if (!win)
        {
            _player.RestoreRagdoll();
        }
        else
        {
            _enemy.RestoreRagdoll();
        }
        _playerTransform.position = _playerStartPoint.position;

        Invoke(nameof(ShowUI), 1);
    }

    private void ShowUI()
    {
        _UIElementsToShow.ForEach(element => element.SetActive(true));
        _playerHealthBar.SetActive(true);
        _touchable.SetActive(true);
    }
}
