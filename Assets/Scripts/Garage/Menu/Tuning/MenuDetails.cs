using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuDetails : MonoBehaviour
{
    [SerializeField] private Slider _sliderDetails;
    [SerializeField] private Image _imageTuning;
    [SerializeField] private ButtonBuyCar _buttonBuyTuning;

    private TuningDetails _tuningDetails;

    public event UnityAction<TuningDetails> TryBuyedDetail;

    private void OnEnable()
    {
        _sliderDetails.onValueChanged.AddListener(OnValueChangeSlider);
        _buttonBuyTuning.ClicledButton += OnClickedButtonBuy;
    }

    private void OnDisable()
    {
        _sliderDetails.onValueChanged.RemoveListener(OnValueChangeSlider);
        _buttonBuyTuning.ClicledButton -= OnClickedButtonBuy;
    }

    public void ShowMenu(bool isShow)
    {
        _sliderDetails.gameObject.SetActive(isShow);
        _imageTuning.gameObject.SetActive(isShow);
        _buttonBuyTuning.gameObject.SetActive(isShow);
    }

    public void ChangeDetails(TuningDetails tuningDetails)
    {
        _tuningDetails = tuningDetails;

        _imageTuning.sprite = _tuningDetails.SpriteTuning;
        _sliderDetails.maxValue = _tuningDetails.CountTunings - 1;
        _sliderDetails.value = _tuningDetails.CurrentIndexTuning;
    }

    public void CahngeVisibilityButton()
    {
        _buttonBuyTuning.ChangeTextPrice(_tuningDetails.TemporaryTuning.IsBuyed, _tuningDetails.TemporaryTuning.Price);

        if (_tuningDetails.TemporaryTuning != _tuningDetails.CurrentTuning)
            _buttonBuyTuning.ChangeVisibility(true);
        else
            _buttonBuyTuning.ChangeVisibility(false);
    }

    private void OnValueChangeSlider(float value)
    {
        _tuningDetails.ShowTuning(Convert.ToInt32(value));
        
        CahngeVisibilityButton();
    }

    private void OnClickedButtonBuy()
    {
        if (_tuningDetails.TemporaryTuning.IsBuyed == false)
            TryBuyedDetail?.Invoke(_tuningDetails);
        else
            _tuningDetails.ChangeCurrentTuning();
    }
}