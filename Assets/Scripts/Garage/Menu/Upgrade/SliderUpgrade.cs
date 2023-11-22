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
      ChangeValue(false);

    if (_temporaryCountUpgrade > _slider.value)
      ChangeValue(true);
  }

  private void ChangeValue(bool isDown)
  {
    while (_temporaryCountUpgrade != Convert.ToInt32(_slider.value))
    {
      if (isDown == true)
        ChangeDownValue();
      else
        ChangeUpValue();
    }
  }

  private void ChangeUpValue()
  {
    ChangedValue?.Invoke(_direction, _temporaryCountUpgrade, _temporaryCountUpgrade + 1);
    _temporaryCountUpgrade++;
  }

  private void ChangeDownValue()
  {
    _temporaryCountUpgrade--;
    ChangedValue?.Invoke(-_direction, _temporaryCountUpgrade, _temporaryCountUpgrade);
  }
}