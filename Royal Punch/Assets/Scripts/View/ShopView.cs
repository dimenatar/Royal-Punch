using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private DamageUpgradeManager _damageUpgradeManager;
    [SerializeField] private HealthUpgradeManager _healthUpgradeManager;
    [SerializeField] private UserMoney _userMoney;

    [SerializeField] private UpgradeBundle _upgradeBundle;

    [SerializeField] private TextMeshProUGUI _healthLevel;
    [SerializeField] private TextMeshProUGUI _healthPrice;

    [SerializeField] private TextMeshProUGUI _damageLevel;
    [SerializeField] private TextMeshProUGUI _damagePrice;

    [SerializeField] private Image _healthNotEnoghtMoney;
    [SerializeField] private Image _damageNotEnoughtMoney;

    [SerializeField] private Button _health;
    [SerializeField] private Button _damage;

    [SerializeField] private GameObject _healthCoin;
    [SerializeField] private GameObject _damageCoin;

    private void Awake()
    {
        _healthUpgradeManager.OnInitialised += DisplayHealth;
        _damageUpgradeManager.OnInitialised += DisplayDamage;
    }

    public void DisplayUpgrades()
    {
        DisplayHealth();
        DisplayDamage();
    }

    public void HealthClick()
    {
        if (_upgradeBundle.IsLastHealthUpgrade(_healthUpgradeManager.HealthUpgrade.Order - 1)) return;
        if (_userMoney.IsEnoughtMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext))
        {
            _userMoney.ReduceMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext);
            _healthUpgradeManager.HealthUpgrade = _upgradeBundle.GetHealth(_healthUpgradeManager.HealthUpgrade.Order);
            DisplayUpgrades();
        }
    }

    public void DamageClick()
    {
        if (_upgradeBundle.IsLastDamageUpgrade(_damageUpgradeManager.CurrentUpgrade.Order - 1)) return;
        if (_userMoney.IsEnoughtMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext))
        {
            _userMoney.ReduceMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext);
            _damageUpgradeManager.CurrentUpgrade = _upgradeBundle.GetDamage(_damageUpgradeManager.CurrentUpgrade.Order);
            DisplayUpgrades();
        }
        else
        {
            _damageNotEnoughtMoney.gameObject.SetActive(true);
        }
    }

    private void DisplayHealth()
    {
        if (!_upgradeBundle.IsLastHealthUpgrade(_healthUpgradeManager.HealthUpgrade.Order - 1))
        {
            _healthLevel.text = $"LV. {_healthUpgradeManager.HealthUpgrade.Order}";
            _healthPrice.text = _healthUpgradeManager.HealthUpgrade.PriceToNext.ToString();

            if (!_userMoney.IsEnoughtMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext))
            {
                _healthNotEnoghtMoney.gameObject.SetActive(true);
            }
            else
            {
                _healthNotEnoghtMoney.gameObject.SetActive(false);
            }
        }
        else
        {
            _healthLevel.text = $"LV. MAX";
            _healthPrice.gameObject.SetActive(false);
            _healthCoin.SetActive(false);
            _health.enabled = false;
        }
    }

    private void DisplayDamage()
    {
        if (!_upgradeBundle.IsLastDamageUpgrade(_damageUpgradeManager.CurrentUpgrade.Order - 1))
        {
            _damageLevel.text = $"LV. {_damageUpgradeManager.CurrentUpgrade.Order}";
            _damagePrice.text = _damageUpgradeManager.CurrentUpgrade.PriceToNext.ToString();

            if (!_userMoney.IsEnoughtMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext))
            {
                _damageNotEnoughtMoney.gameObject.SetActive(true);
            }
            else
            {
                _damageNotEnoughtMoney.gameObject.SetActive(false);
            }
        }
        else
        {
            _damageLevel.text = $"LV. MAX";
            _damagePrice.gameObject.SetActive(false);
            _damageCoin.SetActive(false);
            _damage.enabled = false;
        }
    }
}
