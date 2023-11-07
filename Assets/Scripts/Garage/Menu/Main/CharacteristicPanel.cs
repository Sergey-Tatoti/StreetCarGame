using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicPanel : MonoBehaviour
{
    [SerializeField] private Slider _slidersSpeed;
    [SerializeField] private Slider _sliderDownForce;
    [SerializeField] private Slider _sliderBrakeTorque;
    [SerializeField] private Slider _sliderHighSpeedSteerAngleAtspeed;
    [SerializeField] private MaxCharacteristicCars _maxCharacteristicCars;

    private CarCharacteristic _carCharacteristic;

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