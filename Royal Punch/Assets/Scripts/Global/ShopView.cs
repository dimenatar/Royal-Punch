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

    private void Start()
    {
        DisplayUpgrades();
    }

    private void DisplayUpgrades()
    {



        //check if user has money for each upgrage, if not - cover up upgrade



    }

    private void DisplayHealth()
    {
        if (!_upgradeBundle.IsLastHealthUpgrade(_healthUpgradeManager.HealthUpgrade.Order - 1))
        {
            _healthLevel.text = $"LV. {_healthUpgradeManager.HealthUpgrade.Order}";
            _healthPrice.text = _healthUpgradeManager.HealthUpgrade.PriceToNext.ToString();
        }
        if (!_userMoney.IsEnoughtMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext))
        {

        }
    }

    private void DisplayDamage()
    {
        if (!_upgradeBundle.IsLastDamageUpgrade(_damageUpgradeManager.CurrentUpgrade.Order - 1))
        {

        }

        _damageLevel.text = $"LV. {_damageUpgradeManager.CurrentUpgrade.Order}";
        _damagePrice.text = _damageUpgradeManager.CurrentUpgrade.PriceToNext.ToString();

        if (!_userMoney.IsEnoughtMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext))
        {

        }
    }

    public void HealthClick()
    {
        if (_userMoney.IsEnoughtMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext))
        {
            _userMoney.ReduceMoney(_healthUpgradeManager.HealthUpgrade.PriceToNext);

            //if ugrage isn't last, we display next stage of it
            if (!_upgradeBundle.IsLastHealthUpgrade(_healthUpgradeManager.HealthUpgrade.Order - 1))
            {
                _healthUpgradeManager.HealthUpgrade = _upgradeBundle.GetHealth(_healthUpgradeManager.HealthUpgrade.Order);
            }
            DisplayHealth();
        }
    }

    public void DamageClick()
    {
        if (_userMoney.IsEnoughtMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext))
        {
            _userMoney.ReduceMoney(_damageUpgradeManager.CurrentUpgrade.PriceToNext);

            //if ugrage isn't last, we display next stage of it
            if (!_upgradeBundle.IsLastDamageUpgrade(_damageUpgradeManager.CurrentUpgrade.Order - 1))
            {
                _damageUpgradeManager.CurrentUpgrade = _upgradeBundle.GetDamage(_damageUpgradeManager.CurrentUpgrade.Order);
            }
            DisplayDamage();
        }
    }
}
