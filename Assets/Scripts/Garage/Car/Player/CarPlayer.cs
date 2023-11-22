using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RCC_CarControllerV3))]
[RequireComponent(typeof(CarCountUpgrades))]
[RequireComponent(typeof(CarCharacteristic))]
[RequireComponent(typeof(CarPriceUpgrade))]

public class CarPlayer : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string DownForce = "DownForce";
    private const string BrakeTorque = "BrakeTorque";
    private const string HighSpeedSteer = "HighSpeedSteer";

    [SerializeField] private RCC_CarControllerV3 _car;
    [SerializeField] private CarCountUpgrades _countUpgrades;
    [SerializeField] private CarCharacteristic _characteristic;
    [SerializeField] private CarPriceUpgrade _priceUpgrade;
    [SerializeField] private CarDrowingDetails _carDrowing;
    [SerializeField] private CarTuning _carTuning;
    [SerializeField] private int _id;
    [SerializeField] private int _priceChangeColor;
    [SerializeField] private int _priceCar;
    [SerializeField] private bool _isBuyed = false;

    public int Id => _id;
    public int PriceChangeColor => _priceChangeColor;
    public int PriceCar => _priceCar;
    public bool IsBuyed => _isBuyed;

    public event UnityAction<int> Saved;
    public event UnityAction<int> Loaded;

    private void OnValidate()
    {
        if (_car != null)
        {
            _priceUpgrade.FillListPrices(Speed, _countUpgrades.MaxCountUpgradeSpeed);
            _priceUpgrade.FillListPrices(DownForce, _countUpgrades.MaxCountUpgradeDownForce);
            _priceUpgrade.FillListPrices(BrakeTorque, _countUpgrades.MaxCountUpgradeBrakeTorque);
            _priceUpgrade.FillListPrices(HighSpeedSteer, _countUpgrades.MaxCountUpgradeHighSpeedSteer);
        }
    }

    private void Awake()
    {
        _carTuning.SetCurrentTunings();
        _characteristic.FillCharacteristics(_car);
        _carDrowing.SetCurrentColors();

        Loaded?.Invoke(_id);
        Saved?.Invoke(_id);
    }

    public void ChangeCurrentCountUpgrade(int currentUpgradeSpeed, int currentUpgradeDownForce, int currentUpgradeBrakeTorque, int currentUpgradeHighSpeedSteer)
    {
        _characteristic.ChangeValue(_countUpgrades.CountUpgradeSpeed, _countUpgrades.MaxCountUpgradeSpeed, currentUpgradeSpeed, Speed);
        _characteristic.ChangeValue(_countUpgrades.CountUpgradeDownForce, _countUpgrades.MaxCountUpgradeDownForce, currentUpgradeDownForce, DownForce);
        _characteristic.ChangeValue(_countUpgrades.CountUpgradeBrakeTorque, _countUpgrades.MaxCountUpgradeBrakeTorque, currentUpgradeBrakeTorque, BrakeTorque);
        _characteristic.ChangeValue(_countUpgrades.CountUpgradeHighSpeedSteer, _countUpgrades.MaxCountUpgradeHighSpeedSteer, currentUpgradeHighSpeedSteer, HighSpeedSteer);

        _countUpgrades.ChangeCurrentCount(currentUpgradeSpeed, currentUpgradeDownForce, currentUpgradeBrakeTorque, currentUpgradeHighSpeedSteer);
    }

    public void BuyCar()
    {
        _isBuyed = true;
        SaveBuy();
    }

    public void SaveBuy() => Saved?.Invoke(_id);

    public void ChangeData(bool isBuyed) => _isBuyed = isBuyed;
}