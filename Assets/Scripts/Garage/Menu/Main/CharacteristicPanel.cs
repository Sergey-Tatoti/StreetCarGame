using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicPanel : MonoBehaviour
{
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private UpgradeMenu _updrageMenu;
    [SerializeField] private MaxCharacteristicCars _maxCharacteristicCars;
    [SerializeField] private Slider _slidersSpeed;
    [SerializeField] private Slider _sliderDownForce;
    [SerializeField] private Slider _sliderBrakeTorque;
    [SerializeField] private Slider _sliderHighSpeedSteerAngleAtspeed;

    private CarCharacteristic _carCharacteristic;

    private void OnEnable() 
    {
        _shopMenu.ChangedCar += ShowCharacteristic;
        _updrageMenu.ChangedCharacteristic += ShowCharacteristic;
    }

    private void OnDisable() 
    {
        _shopMenu.ChangedCar -= ShowCharacteristic;
        _updrageMenu.ChangedCharacteristic -= ShowCharacteristic;
    }

    public void ShowCharacteristic(CarPlayer car)
    {
        _carCharacteristic = car.gameObject.GetComponent<CarCharacteristic>();
        
        SetMaxValue();
        SetCurrentValue(_carCharacteristic);
    }

    private void SetMaxValue()
    {
        _slidersSpeed.maxValue = _maxCharacteristicCars.MaxSpeedCars;
        _sliderDownForce.maxValue = _maxCharacteristicCars.MaxDownForceCars;
        _sliderBrakeTorque.maxValue = _maxCharacteristicCars.MaxBrakeTorqueCars;
        _sliderHighSpeedSteerAngleAtspeed.maxValue = _maxCharacteristicCars.MaxHighSpeedSteerCars;
    }

    private void SetCurrentValue(CarCharacteristic characteristic)
    {
        _slidersSpeed.value = characteristic.CurrentSpeed;
        _sliderDownForce.value = characteristic.CurrentDownForce;
        _sliderBrakeTorque.value = characteristic.CurrentBrakeTorque;
        _sliderHighSpeedSteerAngleAtspeed.value = characteristic.CurrentHighSpeedSteer;
    }
}