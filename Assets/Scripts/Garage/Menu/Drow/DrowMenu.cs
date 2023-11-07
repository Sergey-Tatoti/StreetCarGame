using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrowMenu : MonoBehaviour
{
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private DrowPanel _drowPanel;
    [SerializeField] private ButtonBuyDrow _buttonBuyDrow;
    [SerializeField] private Button _buttonExit;
    [SerializeField] private Player _player;

    private DrowCar _drowCar;
    private CarPlayer _car;
    private float _currentColorR = 0;
    private float _currentColorG = 0;
    private float _currentColorB = 0;
    private float _currentColorM = 0;

    private void OnEnable()
    {
        _buttonBuyDrow.ClicledButton += TryBuyColor;
        _buttonExit.onClick.AddListener(ResetValues);
        _drowPanel.ChangedColorR += OnChangedValueR;
        _drowPanel.ChangedColorG += OnChangedValueG;
        _drowPanel.ChangedColorB += OnChangedValueB;
        _drowPanel.ChangedColorM += OnChangedValueM;
        _shopMenu.ChangedCar += ChangedCar;
    }

    private void OnDisable()
    {
        _buttonBuyDrow.ClicledButton -= TryBuyColor;
        _buttonExit.onClick.RemoveListener(ResetValues);
        _drowPanel.ChangedColorR -= OnChangedValueR;
        _drowPanel.ChangedColorG -= OnChangedValueG;
        _drowPanel.ChangedColorB -= OnChangedValueB;
        _drowPanel.ChangedColorM -= OnChangedValueM;
        _shopMenu.ChangedCar -= ChangedCar;
    }

    private void ChangedCar(CarPlayer car)
    {
        _car = car;
        _drowCar = car.GetComponent<DrowCar>();

        _currentColorR = _drowCar.CurrentColorR;
        _currentColorG = _drowCar.CurrentColorG;
        _currentColorB = _drowCar.CurrentColorB;
        _currentColorM = _drowCar.CurrentColorM;

        _drowPanel.SetValueColors(_currentColorR, _currentColorG, _currentColorB, _currentColorM);
    }

    private void TryBuyColor()
    {
        if (_player.Money >= _car.PriceChangeColor)
        {
            _player.Buy(_car.PriceChangeColor);
            _drowCar.SetValueColors(_currentColorR, _currentColorG, _currentColorB, _currentColorM);
            _buttonBuyDrow.ChangePrice(0);
        }
    }

    private void ResetValues()
    {
        _currentColorR = _drowCar.CurrentColorR;
        _currentColorG = _drowCar.CurrentColorG;
        _currentColorB = _drowCar.CurrentColorB;
        _currentColorM = _drowCar.CurrentColorM;

        ChangeColor();
        _drowPanel.SetValueColors(_currentColorR, _currentColorG, _currentColorB, _currentColorM);
        _buttonBuyDrow.ChangePrice(0);
    }

    private void OnChangedValueR(float value)
    {
        _currentColorR = value;
        ChangeColor();
        _buttonBuyDrow.ChangePrice(_car.PriceChangeColor);
    }

    private void OnChangedValueG(float value)
    {
        _currentColorG = value;
        ChangeColor();
        _buttonBuyDrow.ChangePrice(_car.PriceChangeColor);
    }

    private void OnChangedValueB(float value)
    {
        _currentColorB = value;
        ChangeColor();
        _buttonBuyDrow.ChangePrice(_car.PriceChangeColor);
    }

    private void OnChangedValueM(float value)
    {
        _currentColorM = value;
        ChangeColor();
        _buttonBuyDrow.ChangePrice(_car.PriceChangeColor);
    }

    private void ChangeColor()
    {
        for (int i = 0; i < _drowCar.CountPieceCar; i++)
        {
            _drowCar.GetPieceCar(i).GetComponent<Renderer>().material.color = new Color(_currentColorR, _currentColorG, _currentColorB, 0);
            _drowCar.GetPieceCar(i).GetComponent<Renderer>().material.SetFloat("_Metallic", _currentColorM);
        }
    }
}