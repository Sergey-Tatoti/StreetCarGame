using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonBuyCar : MonoBehaviour
{
    private const string Equip = "Equip";
    private const string Valuta = "$";
    
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _textPrice;

    public event UnityAction ClicledButton;

    private void OnEnable()
    {
        _button.onClick.AddListener(() => ClicledButton?.Invoke()); ;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => ClicledButton?.Invoke()); ;
    }

    public void ChangeTextPrice(bool isBuyed, int price = 0)
    {
        if (isBuyed == true)
            _textPrice.text = Equip;
        else
            _textPrice.text = price.ToString() + Valuta;
    }

    public void ChangeVisibility(bool isShow)
    {
        _button.interactable = isShow;
    }
}