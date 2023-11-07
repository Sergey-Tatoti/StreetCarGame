using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeValueAndPrice : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string DownForce = "DownForce";
    private const string BrakeTorque = "BrakeTorque";
    private const string HighSpeedSteer = "HighSpeedSteer";

    private int _currentUpgradeSpeed;
    private int _currentUpgradeDownForce;
    private int _currentUpgradeBrakeTorque;
    private int _currentUpgradeHighSpeedSteer;
    private int _currentPrice = 0;
    private CarCountUpgrades _carCountUprages;
    private CarPriceUpgrade _carPriceUpgrade;

    public int CurrentPrice => _currentPrice;

    public void SetUpgradesCar(CarPlayer car)
    {
        _carCountUprages = car.gameObject.GetComponent<CarCountUpgrades>();
        _carPriceUpgrade = car.gameObject.GetComponent<CarPriceUpgrade>();
    }

    public void ChangeUpgrade(CarPlayer car)
    {
        _currentPrice = 0;
        
        car.ChangeCurrentCountUpgrade(_currentUpgradeSpeed, _currentUpgradeDownForce, _currentUpgradeBrakeTorque, _currentUpgradeHighSpeedSteer);
    }

    public void ResetValue()
    {
        _currentPrice = 0;

        _currentUpgradeSpeed = _carCountUprages.CountUpgradeSpeed;
        _currentUpgradeDownForce = _carCountUprages.CountUpgradeDownForce;
        _currentUpgradeBrakeTorque = _carCountUprages.CountUpgradeBrakeTorque;
        _currentUpgradeHighSpeedSteer = _carCountUprages.CountUpgradeHighSpeedSteer;
    }

    public void ChangedValue(string characteristic, int currentUpdate)
    {
        if (characteristic == Speed)
            _currentUpgradeSpeed = currentUpdate;
        else if (characteristic == DownForce)
            _currentUpgradeSpeed = currentUpdate;
        else if (characteristic == BrakeTorque)
            _currentUpgradeSpeed = currentUpdate;
        else if (characteristic == HighSpeedSteer)
            _currentUpgradeSpeed = currentUpdate;
    }

    public void ChangedPrice(string characteristic, int index, int direction)
    {
        if (characteristic == Speed)
            _currentPrice += _carPriceUpgrade.GetPriceUpgrade(index, Speed) * direction;
        else if (characteristic == DownForce)
            _currentPrice += _carPriceUpgrade.GetPriceUpgrade(index, DownForce) * direction;
        else if (characteristic == BrakeTorque)
            _currentPrice += _carPriceUpgrade.GetPriceUpgrade(index, BrakeTorque) * direction;
        else if (characteristic == HighSpeedSteer)
            _currentPrice += _carPriceUpgrade.GetPriceUpgrade(index, HighSpeedSteer) * direction;
    }
}