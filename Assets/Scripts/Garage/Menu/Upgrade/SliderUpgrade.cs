using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderUpgrade : MonoBehaviour
{
  [SerializeField] private Slider _slider;

  private int _currentCountUpgrade;
  private int _temporaryCountUpgrade;
  private int _direction = 1;

  public event UnityAction<int, int, int> ChangedValue;

  private void OnEnable()
  {
    _slider.onValueChanged.AddListener(TryChangeValue);
  }

  private void OnDisable()
  {
    _slider.onValueChanged.RemoveListener(TryChangeValue);
  }

  public void SetValue(int currentUpdate, int maxUpdate)
  {
    _slider.value = currentUpdate;
    _currentCountUpgrade = currentUpdate;
    _temporaryCountUpgrade = currentUpdate;
    _slider.maxValue = maxUpdate;
  }

  public void ResetValue(int currentUpdate)
  {
    _slider.value = currentUpdate;
  }

  private void TryChangeValue(float value)
  {
    if (_slider.value < _currentCountUpgrade)
      _slider.value = _currentCountUpgrade;
    else
      ChangeValueDirection();
  }

  private void ChangeValueDirection()
  {
    if (_temporaryCountUpgrade < _slider.value)
      ChangeUpValue();

    if (_temporaryCountUpgrade > _slider.value)
      ChangeDownValue();
  }

  private void ChangeUpValue()
  {
    while (_temporaryCountUpgrade != Convert.ToInt32(_slider.value))
    {
      ChangedValue?.Invoke(_direction, _temporaryCountUpgrade, _temporaryCountUpgrade + 1);
      _temporaryCountUpgrade++;
    }
  }

  private void ChangeDownValue()
  {
    while (_temporaryCountUpgrade != Convert.ToInt32(_slider.value))
    {
      _temporaryCountUpgrade--;
      ChangedValue?.Invoke(-_direction, _temporaryCountUpgrade, _temporaryCountUpgrade);
    }
  }
}

