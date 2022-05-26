using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterHealthView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private Character _character;
    [SerializeField] private Image _mainSlide;
    [SerializeField] private Image _additionalSlide;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private GameObject _healthBar;

    #region Animation fields
    [SerializeField] private float _animatedTextScaleMultiplicator = 1.2f;
    [SerializeField] private float _timeToScaleText = 0.1f;
    [SerializeField] private float _timeToUnscaleText = 0.3f;

    [SerializeField] private float _delayToReduceYellowBar = 0.5f;
    [SerializeField] private float _yellowBarAnimationDuration = 0.3f;
    #endregion

    private Vector3 _startHealthScale;
    private Vector3 _animatedHealthScale;

    private RectTransform _healthRect;

    private void Awake()
    {
        _timer.OnTime += ReduceYellowBar;
        _character.OnHealthChanged += UpdateHealth;
        _character.OnInitialised += Initialise;
        _character.OnDied += () => _healthBar.SetActive(false);
    }

    private void Start()
    {
        _timer.Initialise(_delayToReduceYellowBar);
        _startHealthScale = _health.GetComponent<RectTransform>().sizeDelta;
        _animatedHealthScale = _startHealthScale * _animatedTextScaleMultiplicator;
        _healthRect = _health.GetComponent<RectTransform>();
    }

    public void Initialise(int startHealth)
    {
        _health.text = startHealth.ToString();
        _mainSlide.fillAmount = 1;
        _additionalSlide.fillAmount = 1;
        _healthBar.SetActive(true);
    }

    private void UpdateHealth(int value)
    {
        //_healthRect.DOScale(_animatedHealthScale, _timeToScaleText).OnComplete(() => _healthRect.DOScale(_startHealthScale, _timeToUnscaleText));
        _health.text = value.ToString();
        _mainSlide.fillAmount = (float)value / _character.MaxHealth;
        if (!_timer.IsStarted)
        {
            _timer.StartTimer();
        }
        else
        {
            _timer.UpdateTimer();
        }
    }

    private void ReduceYellowBar()
    {
        _additionalSlide.DOFillAmount(_mainSlide.fillAmount, _yellowBarAnimationDuration);
    }
}
