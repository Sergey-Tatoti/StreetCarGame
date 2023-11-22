using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SwitcherCar))]

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SwitcherCar _switcherCar;
    [SerializeField] private ButtonBuyCar _buttonBuyCar;
    [SerializeField] private ProtectPanel _protectPanel;

    private CarPlayer _currentCar;

    public event UnityAction<CarPlayer> ChangedCar;

    private void OnEnable()
    {
        _switcherCar.SwitchedCar += OnSwitchedCar;
        _buttonBuyCar.ClicledButton += OnClickButtonBuy;
    }

    private void OnDisable()
    {
        _switcherCar.SwitchedCar -= OnSwitchedCar;
        _buttonBuyCar.ClicledButton -= OnClickButtonBuy;
    }

    private void Start()
    {
        _currentCar = _player.CurrentCar;
        _switcherCar.SetValueCars(_player.IndexCurrentCar, _player.CountCars);
        _buttonBuyCar.ChangeTextPrice(true);

        ChangedCar?.Invoke(_currentCar);
    }

    public bool TrySellCar(int price)
    {
        bool haveMoney = false;

        if (_player.Money >= price)
        {
            haveMoney = true;
            _player.Buy(price);
        }

        return haveMoney;
    }

    private void OnSwitchedCar(int index, int direction)
    {
        _player.ShowCar(index, direction);
        _currentCar = _player.GetCar(index);

        ChangeBuyButton();
        ChangeProtectShop();

        ChangedCar?.Invoke(_currentCar);
    }

    private void ChangeBuyButton()
    {
        _buttonBuyCar.ChangeTextPrice(_currentCar.IsBuyed, _currentCar.PriceCar);
        _buttonBuyCar.ChangeVisibility(_currentCar != _player.CurrentCar);
    }

    private void ChangeProtectShop()
    {
        if (_currentCar.IsBuyed == true)
            _protectPanel.gameObject.SetActive(false);
        else
            _protectPanel.gameObject.SetActive(true);
    }

    private void OnClickButtonBuy()
    {
        if (_currentCar.IsBuyed == true)
            ChangeCar();
        else
            SellCar();
    }

    private void SellCar()
    {
        if (TrySellCar(_currentCar.PriceCar) == true)
        {
            _currentCar.BuyCar();
            _protectPanel.gameObject.SetActive(false);
            ChangeCar();
        }
    }

    private void ChangeCar()
    {
        _player.ChangeCar(_currentCar);
        _buttonBuyCar.ChangeVisibility(false);
    }
}