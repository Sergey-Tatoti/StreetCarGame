using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCountUpgrades : MonoBehaviour
{
    [SerializeField] private int _maxCountUpgradeSpeed;
    [SerializeField] private int _maxCountUpgradeDownForce;
    [SerializeField] private int _maxCountUpgradeBrakeTorque;
    [SerializeField] private int _maxCountUpgradeHighSpeedSteer;

    private int _countUpgradeSpeed = 0;
    private int _countUpgradeDownForce = 0;
    private int _countUpgradeBrakeTorque = 0;
    private int _countUpgradeHighSpeedSteer = 0;

    public int MaxCountUpgradeSpeed => _maxCountUpgradeSpeed;
    public int MaxCountUpgradeDownForce => _maxCountUpgradeDownForce;
    public int MaxCountUpgradeBrakeTorque => _maxCountUpgradeBrakeTorque;
    public int MaxCountUpgradeHighSpeedSteer => _maxCountUpgradeHighSpeedSteer;

    public int CountUpgradeSpeed => _countUpgradeSpeed;
    public int CountUpgradeDownForce => _countUpgradeDownForce;
    public int CountUpgradeBrakeTorque => _countUpgradeBrakeTorque;
    public int CountUpgradeHighSpeedSteer => _countUpgradeHighSpeedSteer;

    public void ChangeCurrentCount(int currentUpgradeSpeed, int currentUpgradeDownForce, int currentUpgradeBrakeTorque, int currentUpgradeHighSpeedSteer)
    {
        _countUpgradeSpeed = currentUpgradeSpeed;
        _countUpgradeDownForce = currentUpgradeDownForce;
        _countUpgradeBrakeTorque = currentUpgradeBrakeTorque;
        _countUpgradeHighSpeedSteer = currentUpgradeHighSpeedSteer;
    }
}