using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private TextMeshProUGUI _moneyText;

    [SerializeField] private Character _enemy;
    [SerializeField] private Character _player;

    [SerializeField] private UserMoney _money;
    [SerializeField] private LevelResetter _levelResetter;

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
        if (win)
        {
            _result.text = "SUCCESS!";
        }
        else
        {
            _result.text = "FAIL!";
        }
        _reward = _money.CalculateReward(_enemy);
        _moneyText.text = _reward.ToString();
        _panel.SetActive(true);
       // Time.timeScale = 0;
    }

    public void ClaimClick()
    {
        HidePanel();
        _money.AddMoney(_reward);
        _levelResetter.ResetLevel(_isWin);
    }

    private void HidePanel()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }


    private void PlayerDied() => ShowPanel(false);
    private void EnemyDied() => ShowPanel(true);
}
