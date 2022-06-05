using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private EnemySpecial _enemySpecial;
    [SerializeField] private PlayerFight _playerFight;
    [SerializeField] private EnemyFight _enemyFight;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private TouchPosition _touchPosition;

    [SerializeField] private float _timeToAnimate = 0.3f;

    public void PauseClick()
    {
        _enemySpecial.StopSpecials();
        _playerFight.enabled = false;
        _enemyFight.enabled = false;
        _pauseMenu.GetComponent<RectTransform>().localScale = Vector3.zero;
        _pauseMenu.SetActive(true);
        _pauseMenu.GetComponent<RectTransform>().DOScale(1, _timeToAnimate).SetUpdate(true).OnComplete(() => _pauseMenu.transform.DOPunchScale(new Vector3(2,2,2), 0.2f, 2)).SetUpdate(true);
        Time.timeScale = 0;
        _touchPosition.DisalbeTouch();
    }

    public void ResumeClick()
    {
        _enemySpecial.Initialise();
        _playerFight.enabled = true;
        _enemyFight.enabled = true;
        Time.timeScale = 1;
        _pauseMenu.GetComponent<RectTransform>().DOScale(0, _timeToAnimate).OnComplete(() => _pauseMenu.SetActive(false));
       // _pauseMenu.SetActive(false);
        _touchPosition.Enabletouch();
    }

    public void HomeClick()
    {
        _levelLoader.Reload();
        Time.timeScale = 1;
    }
}
