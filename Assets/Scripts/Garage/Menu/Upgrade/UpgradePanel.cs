using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradePanel : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string DownForce = "DownForce";
    private const string BrakeTorque = "BrakeTorque";
    private const string HighSpeedSteer = "HighSpeedSteer";

    [SerializeField] private SliderUpgrade _sliderSpeed;
    [SerializeField] private SliderUpgrade _sliderDownForce;
    [SerializeField] private SliderUpgrade _sliderBrakeTorque;
    [SerializeField] private SliderUpgrade _sliderHighSpeedSteer;

    private CarCountUpgrades _carCountUprages;

    public event UnityAction<string, int, int, int> ChangedValueUpdate;

    public void SetUpgradesCar(CarPlayer car)
    {
        _carCountUprages = car.GetComponent<CarCountUpgrades>();
    }

    public void SetValuesSliders()
    {
        SetValues();

        _sliderSpeed.ChangedValue += OnSliderChangedValueSpeed;
        _sliderDownForce.ChangedValue += OnSliderChangedDownForce;
        _sliderBrakeTorque.ChangedValue += OnSliderChangedBrakeTorque;
        _sliderHighSpeedSteer.ChangedValue += OnSliderChangedHighSpeedSteer;
    }

    public void ResetValuesSliders()
    {
        SetValues();

        _sliderSpeed.ChangedValue -= OnSliderChangedValueSpeed;
        _sliderDownForce.ChangedValue -= OnSliderChangedDownForce;
        _sliderBrakeTorque.ChangedValue -= OnSliderChangedBrakeTorque;
        _sliderHighSpeedSteer.ChangedValue -= OnSliderChangedHighSpeedSteer;
    }

    public void SetValues()
    {
        _sliderSpeed.SetValue(_carCountUprages.CountUpgradeSpeed, _carCountUprages.MaxCountUpgradeSpeed);
        _sliderDownForce.SetValue(_carCountUprages.CountUpgradeDownForce, _carCountUprages.MaxCountUpgradeDownForce);
        _sliderBrakeTorque.SetValue(_carCountUprages.CountUpgradeBrakeTorque, _carCountUprages.MaxCountUpgradeBrakeTorque);
        _sliderHighSpeedSteer.SetValue(_carCountUprages.CountUpgradeHighSpeedSteer, _carCountUprages.MaxCountUpgradeHighSpeedSteer);
    }

    private void OnSliderChangedValueSpeed(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdate?.Invoke(Speed, direction, index, currentUpdate);
    }

    private void OnSliderChangedDownForce(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdate?.Invoke(DownForce, direction, index, currentUpdate);
    }

    private void OnSliderChangedBrakeTorque(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdate?.Invoke(BrakeTorque, direction, index, currentUpdate);
    }

    private void OnSliderChangedHighSpeedSteer(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdate?.Invoke(HighSpeedSteer, direction, index, currentUpdate);
    }
}