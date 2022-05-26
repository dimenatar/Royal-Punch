using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private EnemySpecial _enemySpecial;
    [SerializeField] private PlayerFight _playerFight;
    [SerializeField] private EnemyFight _enemyFight;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private TouchPosition _touchPosition;

    public void PauseClick()
    {
        _enemySpecial.StopSpecials();
        _playerFight.enabled = false;
        _enemyFight.enabled = false;
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _touchPosition.DisalbeTouch();
    }

    public void ResumeClick()
    {
        _enemySpecial.Initialise();
        _playerFight.enabled = true;
        _enemyFight.enabled = true;
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _touchPosition.Enabletouch();
    }

    public void HomeClick()
    {
        _levelLoader.Reload();
        Time.timeScale = 1;
    }
}
