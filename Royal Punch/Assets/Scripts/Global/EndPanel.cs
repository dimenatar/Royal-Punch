using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private Character _enemy;
    [SerializeField] private Character _player;

    [SerializeField] private UserMoney _money;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private DataLoader _dataLoader;

    [SerializeField] private float _timeToAnimate = 0.3f;
    [SerializeField] private float _delayToShow = 1;

    private bool _isWin;
    private int _reward;

    private void Awake()
    {
        _enemy.OnDied += EnemyDied;
        _player.OnDied += PlayerDied;
    }

    public void ShowPanel(bool win)
    {
        _isWin = win;
        Invoke(nameof(Show), _delayToShow);
       // Time.timeScale = 0;
    }

    public void ClaimClick()
    {
        HidePanel();
        _money.AddMoney(_reward);
        _dataLoader.SaveData();
        _levelLoader.Reload();
    }

    private void HidePanel()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void Show()
    { 
        if (_isWin)
        {
            _result.text = "SUCCESS!";
        }
        else
        {
            _result.text = "FAIL!";
        }
        _reward = _money.CalculateReward(_enemy);
        _moneyText.text = _reward.ToString();
        _panel.GetComponent<RectTransform>().localScale = Vector3.zero;
        _panel.SetActive(true);
        _panel.GetComponent<RectTransform>().DOScale(1, _timeToAnimate);
    }

    private void PlayerDied() => ShowPanel(false);
    private void EnemyDied() => ShowPanel(true);
}
