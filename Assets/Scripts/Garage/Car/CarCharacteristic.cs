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
        _currentSpeed = car.maxspeed;
        _currentBrakeTorque = car.brakeTorque;
        _currentDownForce = car.downForce;
        _currentHighSpeedSteer = car.highspeedsteerAngleAtspeed;
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
    }
}