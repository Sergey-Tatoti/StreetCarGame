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
    
    private float _currentColorR = 0;
    private float _currentColorG = 0;
    private float _currentColorB = 0;
    private float _currentColorM = 0;

    public float CurrentColorR => _currentColorR;
    public float CurrentColorG => _currentColorG;
    public float CurrentColorB => _currentColorB;
    public float CurrentColorM => _currentColorM;

    public event UnityAction ChangedColor;

    public void SetValueColors(DrowingDetails drowingDetails)
    {
        SlidersSetValue(drowingDetails);

        _sliderColorR.ChangedValue += OnChangedValueR;
        _sliderColorG.ChangedValue += OnChangedValueG;
        _sliderColorB.ChangedValue += OnChangedValueB;
        _sliderColorM.ChangedValue += OnChangedValueM;
    }

    public void ResetValueColors(DrowingDetails drowingDetails)
    {
        SlidersSetValue(drowingDetails);

        _sliderColorR.ChangedValue -= OnChangedValueR;
        _sliderColorG.ChangedValue -= OnChangedValueG;
        _sliderColorB.ChangedValue -= OnChangedValueB;
        _sliderColorM.ChangedValue -= OnChangedValueM;
    }

    private void SlidersSetValue(DrowingDetails drowingDetails)
    {
        _currentColorR = drowingDetails.CurrentColorR;
        _currentColorG = drowingDetails.CurrentColorG;
        _currentColorB = drowingDetails.CurrentColorB;
        _currentColorM = drowingDetails.CurrentColorM;

        _sliderColorR.SetValue(drowingDetails.CurrentColorR);
        _sliderColorG.SetValue(drowingDetails.CurrentColorG);
        _sliderColorB.SetValue(drowingDetails.CurrentColorB);
        _sliderColorM.SetValue(drowingDetails.CurrentColorM);
    }

    private void OnChangedValueR(float value)
    {
        _currentColorR = value;
        ChangedColor?.Invoke();
    }
    private void OnChangedValueG(float value)
    {
        _currentColorG = value;
        ChangedColor?.Invoke();
    }
    private void OnChangedValueB(float value)
    {
        _currentColorB = value;
        ChangedColor?.Invoke();
    }
    private void OnChangedValueM(float value)
    {
        _currentColorM = value;
        ChangedColor?.Invoke();
    }
}