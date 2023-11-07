using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private SliderUpgrade _sliderSpeed;
    [SerializeField] private SliderUpgrade _sliderDownForce;
    [SerializeField] private SliderUpgrade _sliderBrakeTorque;
    [SerializeField] private SliderUpgrade _sliderHighSpeedSteer;

    private CarCountUpgrades _carCountUprages;

    public event UnityAction<int, int, int> ChangedValueUpdateSpeed;
    public event UnityAction<int, int, int> ChangedValueUpdateDownForce;
    public event UnityAction<int, int, int> ChangedValueUpdateBrakeTorque;
    public event UnityAction<int, int, int> ChangedValueUpdateHighSpeedSteer;

    public void SetValuesSliders(CarPlayer car)
    {
        _carCountUprages = car.gameObject.GetComponent<CarCountUpgrades>();

        _sliderSpeed.SetValue(_carCountUprages.CountUpgradeSpeed, _carCountUprages.MaxCountUpgradeSpeed);
        _sliderDownForce.SetValue(_carCountUprages.CountUpgradeDownForce, _carCountUprages.MaxCountUpgradeDownForce);
        _sliderBrakeTorque.SetValue(_carCountUprages.CountUpgradeBrakeTorque, _carCountUprages.MaxCountUpgradeBrakeTorque);
        _sliderHighSpeedSteer.SetValue(_carCountUprages.CountUpgradeHighSpeedSteer, _carCountUprages.MaxCountUpgradeHighSpeedSteer);

        Debug.Log(_carCountUprages.CountUpgradeSpeed);

        _sliderSpeed.ChangedValue += OnSliderChangedValueSpeed;
        _sliderDownForce.ChangedValue += OnSliderChangedDownForce;
        _sliderBrakeTorque.ChangedValue += OnSliderChangedBrakeTorque;
        _sliderHighSpeedSteer.ChangedValue += OnSliderChangedHighSpeedSteer;
    }

    public void ResetValuesSliders()
    {
        _sliderSpeed.ResetValue(_carCountUprages.CountUpgradeSpeed);
        _sliderDownForce.ResetValue(_carCountUprages.CountUpgradeDownForce);
        _sliderBrakeTorque.ResetValue(_carCountUprages.CountUpgradeBrakeTorque);
        _sliderHighSpeedSteer.ResetValue(_carCountUprages.CountUpgradeHighSpeedSteer);

        _sliderSpeed.ChangedValue -= OnSliderChangedValueSpeed;
        _sliderDownForce.ChangedValue -= OnSliderChangedDownForce;
        _sliderBrakeTorque.ChangedValue -= OnSliderChangedBrakeTorque;
        _sliderHighSpeedSteer.ChangedValue -= OnSliderChangedHighSpeedSteer;
    }

    public void UpdateValuesSliders()
    {
        _sliderSpeed.SetValue(_carCountUprages.CountUpgradeSpeed, _carCountUprages.MaxCountUpgradeSpeed);
        _sliderDownForce.SetValue(_carCountUprages.CountUpgradeDownForce, _carCountUprages.MaxCountUpgradeDownForce);
        _sliderBrakeTorque.SetValue(_carCountUprages.CountUpgradeBrakeTorque, _carCountUprages.MaxCountUpgradeBrakeTorque);
        _sliderHighSpeedSteer.SetValue(_carCountUprages.CountUpgradeHighSpeedSteer, _carCountUprages.MaxCountUpgradeHighSpeedSteer);
    }

    private void OnSliderChangedValueSpeed(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdateSpeed?.Invoke(direction, index, currentUpdate);
    }

    private void OnSliderChangedDownForce(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdateDownForce?.Invoke(direction, index, currentUpdate);
    }

    private void OnSliderChangedBrakeTorque(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdateBrakeTorque?.Invoke(direction, index, currentUpdate);
    }

    private void OnSliderChangedHighSpeedSteer(int direction, int index, int currentUpdate)
    {
        ChangedValueUpdateHighSpeedSteer?.Invoke(direction, index, currentUpdate);
    }
}