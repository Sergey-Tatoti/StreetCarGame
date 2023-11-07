using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrowPanel : MonoBehaviour
{
    [SerializeField] private SliderColor _sliderColorR;
    [SerializeField] private SliderColor _sliderColorG;
    [SerializeField] private SliderColor _sliderColorB;
    [SerializeField] private SliderColor _sliderColorM;

    public event UnityAction<float> ChangedColorR;
    public event UnityAction<float> ChangedColorG;
    public event UnityAction<float> ChangedColorB;
    public event UnityAction<float> ChangedColorM;



    public void SetValueColors(float _currentColorR, float _currentColorG, float _currentColorB, float _currentColorM)
    {
        _sliderColorR.SetValue(_currentColorR);
        _sliderColorG.SetValue(_currentColorG);
        _sliderColorB.SetValue(_currentColorB);
        _sliderColorM.SetValue(_currentColorM);

        _sliderColorR.ChangedValue += OnChangedValueR;
        _sliderColorG.ChangedValue += OnChangedValueG;
        _sliderColorB.ChangedValue += OnChangedValueB;
        _sliderColorM.ChangedValue += OnChangedValueM;
    }

    private void OnChangedValueR(float value)
    {
        ChangedColorR?.Invoke(value);
    }
    private void OnChangedValueG(float value)
    {
        ChangedColorG?.Invoke(value);
    }
    private void OnChangedValueB(float value)
    {
        ChangedColorB?.Invoke(value);
    }
    private void OnChangedValueM(float value)
    {
        ChangedColorM?.Invoke(value);
    }
}