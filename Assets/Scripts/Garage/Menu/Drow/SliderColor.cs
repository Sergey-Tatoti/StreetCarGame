using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private float _maxValue = 1;

    public event UnityAction<float> ChangedValue;

    private void OnEnable() 
    {
        _slider.onValueChanged.AddListener(ChangedValue);
    }

    private void OnDisable() 
    {
        _slider.onValueChanged.RemoveListener(ChangedValue);
    }

    public void SetValue(float currentValue)
    {
        _slider.value = currentValue;
        _slider.maxValue = _maxValue;
    }
}