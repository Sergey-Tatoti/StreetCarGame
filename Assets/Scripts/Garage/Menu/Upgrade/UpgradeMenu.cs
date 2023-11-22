using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
  [SerializeField] private ShopMenu _shopMenu;
  [SerializeField] private UpgradePanel _upgradePanel;
  [SerializeField] private UpgradeValueAndPrice _upgradeValueAndPrice;
  [SerializeField] private ButtonBuyUpgrade _buttonBuyUpgrade;
  [SerializeField] private Button _buttonUpgrade;
  [SerializeField] private Button _buttonBack;

  private CarPlayer _car;

  public event UnityAction<CarPlayer> ChangedCharacteristic;

  private void OnEnable()
  {
    _buttonUpgrade.onClick.AddListener(OnClickButtonUpgrade);
    _buttonBack.onClick.AddListener(OnClickButtonBack);
    _buttonBuyUpgrade.ClicledButton += TrySellUpgrade;
    _shopMenu.ChangedCar += OnChangedCar;
    _upgradePanel.ChangedValueUpdate += OnChangedValueUpdate;
  }

  private void OnDisable()
  {
    _buttonUpgrade.onClick.RemoveListener(OnClickButtonUpgrade);
    _buttonBack.onClick.RemoveListener(OnClickButtonBack);
    _buttonBuyUpgrade.ClicledButton -= TrySellUpgrade;
    _shopMenu.ChangedCar -= OnChangedCar;
    _upgradePanel.ChangedValueUpdate -= OnChangedValueUpdate;
  }

  private void OnChangedCar(CarPlayer car)
  {
    _car = car;

    _upgradeValueAndPrice.SetUpgradesCar(_car);
    _upgradePanel.SetUpgradesCar(_car);
  }

  private void OnChangedValueUpdate(string characteristic, int direction, int index, int currentUpdate)
  {
    _upgradeValueAndPrice.ChangedPrice(characteristic, index, direction);
    _upgradeValueAndPrice.ChangedValue(characteristic, currentUpdate);
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void OnClickButtonUpgrade()
  {
    _upgradePanel.SetValuesSliders();
  }

  private void OnClickButtonBack()
  {
    _upgradePanel.ResetValuesSliders();
    _upgradeValueAndPrice.ResetValue();
    _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
  }

  private void TrySellUpgrade()
  {
    if (_shopMenu.TrySellCar(_upgradeValueAndPrice.CurrentPrice) == true)
    {
      _upgradeValueAndPrice.ChangeUpgrade(_car);
      _upgradePanel.SetValues();
      _buttonBuyUpgrade.ChangePrice(_upgradeValueAndPrice.CurrentPrice);
      _car.SaveBuy();

      ChangedCharacteristic?.Invoke(_car);
    }
  }
}