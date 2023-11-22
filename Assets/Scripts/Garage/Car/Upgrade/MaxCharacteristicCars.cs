using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCharacteristicCars : MonoBehaviour
{
    [SerializeField] private float _maxSpeedCars = 400;
    [SerializeField] private float _maxDownForceCars = 100;
    [SerializeField] private float _maxBrakeTorqueCars = 1000;
    [SerializeField] private float _maxHighSpeedSteer = 10;

    public float MaxSpeedCars => _maxSpeedCars;
    public float MaxDownForceCars => _maxDownForceCars;
    public float MaxBrakeTorqueCars => _maxBrakeTorqueCars;
    public float MaxHighSpeedSteerCars => _maxHighSpeedSteer;
}