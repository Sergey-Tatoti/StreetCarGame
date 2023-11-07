using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonBuyDrow : MonoBehaviour
{
    [SerializeField] private Button _buttonPrice;
    [SerializeField] private TMP_Text _textPrice;

    public event UnityAction ClicledButton;

    private void OnEnable()
    {
        _buttonPrice.onClick.AddListener(() => ClicledButton?.Invoke()); ;
    }

    private void OnDisable()
    {
        _buttonPrice.onClick.RemoveListener(() => ClicledButton?.Invoke()); ;
    }

    private void Awake()
    {
        _buttonPrice.gameObject.SetActive(false);
    }

    public void ChangePrice(int price)
    {
        if (_buttonPrice.gameObject.activeSelf == false)
            _buttonPrice.gameObject.SetActive(true);

        if (price == 0)
            _buttonPrice.gameObject.SetActive(false);
            
        _textPrice.text = price.ToString();
    }

    public void ChangeVisibility(bool isShow)
    {
        _buttonPrice.gameObject.SetActive(isShow);
    }
}