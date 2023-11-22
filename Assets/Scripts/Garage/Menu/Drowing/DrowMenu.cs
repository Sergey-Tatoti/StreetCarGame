using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DrowPanel))]

public class DrowMenu : MonoBehaviour
{
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private DrowPanel _drowPanel;
    [SerializeField] private ButtonBuyUpgrade _buttonBuyDrow;
    [SerializeField] private Button _buttonChangeColorCar;
    [SerializeField] private Button _buttonChangeColorSaloon;
    [SerializeField] private Button _buttonChangeColorWheels;
    [SerializeField] private Button _buttonExit;

    private CarDrowingDetails _carDrowingDetails;
    private DrowingDetails _drowingDetails;
    private CarPlayer _car;

    private void OnEnable()
    {
        _buttonExit.onClick.AddListener(ResetValues);
        _buttonChangeColorCar.onClick.AddListener(() => SetDrowingDetails(_carDrowingDetails.DrowCar)); ;
        _buttonChangeColorSaloon.onClick.AddListener(() => SetDrowingDetails(_carDrowingDetails.DrowSaloon)); ;
        _buttonChangeColorWheels.onClick.AddListener(() => SetDrowingDetails(_carDrowingDetails.DrowWheels)); ;
        _buttonBuyDrow.ClicledButton += TryBuyColor;
        _drowPanel.ChangedColor += OnChangedColor;
        _shopMenu.ChangedCar += ChangedCar;
    }

    private void OnDisable()
    {
        _buttonExit.onClick.RemoveListener(ResetValues);
        _buttonChangeColorCar.onClick.RemoveListener(() => SetDrowingDetails(_carDrowingDetails.DrowCar)); ;
        _buttonChangeColorSaloon.onClick.RemoveListener(() => SetDrowingDetails(_carDrowingDetails.DrowSaloon)); ;
        _buttonChangeColorWheels.onClick.RemoveListener(() => SetDrowingDetails(_carDrowingDetails.DrowWheels)); ;
        _buttonBuyDrow.ClicledButton -= TryBuyColor;
        _drowPanel.ChangedColor -= OnChangedColor;
        _shopMenu.ChangedCar -= ChangedCar;
    }

    private void ChangedCar(CarPlayer car)
    {
        _car = car;

        SetCarDrowingDetails();
    }

    private void SetCarDrowingDetails()
    {
        foreach (Transform carElements in _car.transform.GetComponentsInChildren<Transform>())
        {
            if (carElements.gameObject.GetComponent<CarDrowingDetails>() != null)
                _carDrowingDetails = carElements.gameObject.GetComponent<CarDrowingDetails>();
        }
    }

    private void SetDrowingDetails(DrowingDetails drowingDetails)
    {
        _drowingDetails = drowingDetails;
        _drowPanel.SetValueColors(drowingDetails);
    }

    private void TryBuyColor()
    {
        if (_shopMenu.TrySellCar(_car.PriceChangeColor) == true)
        {
            _drowingDetails.SetValueColors(_drowPanel.CurrentColorR, _drowPanel.CurrentColorG, _drowPanel.CurrentColorB, _drowPanel.CurrentColorM);
            _buttonBuyDrow.ChangePrice(0);
            _car.SaveBuy();
        }
    }

    private void ResetValues()
    {
        _drowPanel.ResetValueColors(_drowingDetails);
        _buttonBuyDrow.ChangePrice(0);

        _drowingDetails.ChangeColor(_drowPanel.CurrentColorR, _drowPanel.CurrentColorG, _drowPanel.CurrentColorB, _drowPanel.CurrentColorM);
    }

    private void OnChangedColor()
    {
        _drowingDetails.ChangeColor(_drowPanel.CurrentColorR, _drowPanel.CurrentColorG, _drowPanel.CurrentColorB, _drowPanel.CurrentColorM);
        _buttonBuyDrow.ChangePrice(_car.PriceChangeColor);
    }
}