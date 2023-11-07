using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
  private const string Speed = "Speed";
  private const string DownForce = "DownForce";
  private const string BrakeTorque = "BrakeTorque";
  private const string HighSpeedSteer = "HighSpeedSteer";

  [SerializeField] private Player _player;
  [SerializeField] private UpgradePanel _upgradePanel;
  [SerializeField] private UpgradeValueAndPrice _upgradeValueAndPrice;
  [SerializeField] private ShopMenu _shopMenu;
  [SerializeField] private Button _buttonBack;
  [SerializeField] private ButtonBuyUpgrade _buttonBuyUpgrade;

  private CarPlayer _car;

  private void OnEnable()
  {
    _shopMenu.ChangedCar += OnChangedCar;
    _buttonBack.onClick.AddListener(OnClickButtonBack);
    _buttonBuyUpgrade.ClicledButton += TrySellUpgrade;
    _upgradePanel.ChangedValueUpdateSpeed += OnChangedValueUpgradeSpeed;
    _upgradePanel.ChangedValueUpdateDownForce += OnChangedValueUpgradeDownForce;
    _upgradePanel.ChangedValueUpdateBrakeTorque += OnChangedValueUpgradeBrakeTorque;
    _upgradePanel.ChangedValueUpdateHighSpeedSteer += OnChangedValueUpgradeHighSpeedSteer;
  }

  private void OnDisable()
  {
    _shopMenu.ChangedCar -= OnChangedCar;
    _buttonBack.onClick.RemoveListener(OnClickButtonBack);
    _buttonBuyUpgrade.ClicledButton -= TrySellUpgrade;
    _upgradePanel.ChangedValueUpdateSpeed -= OnChangedValueUpgradeSpeed;
    _upgradePanel.ChangedValueUpdateDownForce -= OnChangedValueUpgradeDownForce;
    _upgradePanel.ChangedValueUpdateBrakeTorque -= OnChangedValueUpgradeBrakeTorque;
    _upgradePanel.ChangedValueUpdateHighSpeedSteer -= OnChangedValueUpgradeHighSpeedSteer;
  }

  private void OnChangedCar(CarPlayer car)
  {
    _car = car;
    
    _upgradePanel.SetValuesSliders(_car);
    _upgradeValueAndPrice.SetUpgradesCar(_car);
    _buttonBuyUpgrade.ChangeVisibility(false);
  }

  private void OnChangedValueUpgradeSpeed(int direction, int index, int currentUpdate)
  {
    _upgradeValueAndPrice.ChangedPrice(Speed, index, direction);
    _upgradeValueAndPrice.ChangedValue(Speed, currentUpdate);
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void OnChangedValueUpgradeDownForce(int direction, int index, int currentUpdate)
  {
    _upgradeValueAndPrice.ChangedPrice(DownForce, index, direction);
    _upgradeValueAndPrice.ChangedValue(DownForce, currentUpdate);
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void OnChangedValueUpgradeBrakeTorque(int direction, int index, int currentUpdate)
  {
    _upgradeValueAndPrice.ChangedPrice(BrakeTorque, index, direction);
    _upgradeValueAndPrice.ChangedValue(BrakeTorque, currentUpdate);
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void OnChangedValueUpgradeHighSpeedSteer(int direction, int index, int currentUpdate)
  {
    _upgradeValueAndPrice.ChangedPrice(HighSpeedSteer, index, direction);
    _upgradeValueAndPrice.ChangedValue(HighSpeedSteer, currentUpdate);
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void OnClickButtonBack()
  {
    _upgradePanel.ResetValuesSliders();
    _upgradeValueAndPrice.ResetValue();

    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
    _buttonBuyUpgrade.ChangeVisibility(false);
  }

  private void TrySellUpgrade()
  {
    if (_player.Money >= _upgradeValueAndPrice.CurrentPrice)
    {
      _player.Buy(_upgradeValueAndPrice.CurrentPrice);
      _upgradeValueAndPrice.ChangeUpgrade(_car);
      _upgradePanel.UpdateValuesSliders();
      _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
    }
  }
}