using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCharacteristic : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string DownForce = "DownForce";
    private const string BrakeTorque = "BrakeTorque";
    private const string HighSpeedSteer = "HighSpeedSteer";

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxDownForce;
    [SerializeField] private float _minBrakeTorque;
    [SerializeField] private float _minHighSpeedSteer;

    private RCC_CarControllerV3 _car;
    private float _currentSpeed;
    private float _currentDownForce;
    private float _currentBrakeTorque;
    private float _currentHighSpeedSteer;

    public float MaxSpeed => _maxSpeed;
    public float MaxDownForce => _maxDownForce;
    public float MinBrakeTorque => _minBrakeTorque;
    public float MinHighSpeedSteer => _minHighSpeedSteer;

    public float CurrentSpeed => _currentSpeed;
    public float CurrentDownForce => _currentDownForce;
    public float CurrentBrakeTorque => _currentBrakeTorque;
    public float CurrentHighSpeedSteer => _currentHighSpeedSteer;

    public void FillCharacteristics(RCC_CarControllerV3 car)
    {
        _car = car;

        _currentSpeed = _car.maxspeed;
        _currentDownForce = _car.downForce;
        _currentBrakeTorque = _car.brakeTorque;
        _currentHighSpeedSteer = _car.highspeedsteerAngleAtspeed;
    }

    public void ChangeValue(int currentUpgrade, int maxUpgrade, int receivedUpgrade, string characteristic)
    {
        while (currentUpgrade < receivedUpgrade)
        {
            if (characteristic == Speed)
                _currentSpeed += Convert.ToInt32(_maxSpeed - _currentSpeed) / (maxUpgrade - currentUpgrade);
            else if (characteristic == DownForce)
                _currentDownForce += Convert.ToInt32(_maxDownForce - _currentDownForce) / (maxUpgrade - currentUpgrade);
            else if (characteristic == BrakeTorque)
                _currentBrakeTorque -= Convert.ToInt32(_currentBrakeTorque - _minBrakeTorque) / (maxUpgrade - currentUpgrade);
            else if (characteristic == HighSpeedSteer)
                _currentHighSpeedSteer -= Convert.ToInt32(_currentHighSpeedSteer - _minHighSpeedSteer) / (maxUpgrade - currentUpgrade);
            else
                Debug.Log("Ошибка в указании константы параметра значения CarCharacteristics");

            currentUpgrade++;
        }

        ChangeValueCharacteristics();
    }

    private void ChangeValueCharacteristics()
    {
        if (_car != null)
        {
            _car.maxspeed = _currentSpeed;
            _car.downForce = _currentDownForce;
            _car.brakeTorque = _currentBrakeTorque;
            _car.highspeedsteerAngleAtspeed = _currentHighSpeedSteer;
        }
    }
}