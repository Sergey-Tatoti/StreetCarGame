using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TuningMenu : MonoBehaviour
{
    [SerializeField] private CameraGarage _camera;
    [SerializeField] private ShopMenu _shopMenu;
    [SerializeField] private SwitcherTunings _switcherTunings;
    [SerializeField] private MenuDetails _menuDetails;
    [SerializeField] private Button _buttonTuningMenu;
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonExit;

    private CarPlayer _car;
    private CarTuning _carTuning;

    private void OnEnable()
    {
        _buttonTuningMenu.onClick.AddListener(() => OnShowButtonsTuning(true));
        _buttonExit.onClick.AddListener(onClickButtonExit);
        _buttonBack.onClick.AddListener(onClickButtonBack);
        _shopMenu.ChangedCar += OnChangedCar;
        _switcherTunings.ClicledButtonTuning += OnClicledButtonTunings;
        _menuDetails.TryBuyedDetail += OnTryBuyedDetail;
    }

    private void OnDisable()
    {
        _buttonTuningMenu.onClick.RemoveListener(() => OnShowButtonsTuning(true));
        _buttonExit.onClick.RemoveListener(onClickButtonExit);
        _buttonBack.onClick.RemoveListener(onClickButtonBack);
        _shopMenu.ChangedCar -= OnChangedCar;
        _switcherTunings.ClicledButtonTuning -= OnClicledButtonTunings;
        _menuDetails.TryBuyedDetail -= OnTryBuyedDetail;
    }

    private void OnShowButtonsTuning(bool isShow)
    {
        _switcherTunings.ChangeVisibilityButtons(isShow);
        _camera.LockCamera(isShow);
        _camera.ReturnStartPosition();
    }

    private void onClickButtonExit()
    {
        OnShowButtonsTuning(false);
    }

    private void onClickButtonBack()
    {
        _camera.ReturnStartPosition();
        _switcherTunings.ChangeVisibilityButtons(true);
       _menuDetails.ShowMenu(false);
    }

    private void OnClicledButtonTunings(TuningDetails tuningDetails)
    {
        _switcherTunings.ChangeVisibilityButtons(false);
        _buttonBack.gameObject.SetActive(true);
        _buttonExit.gameObject.SetActive(false);

        _menuDetails.ShowMenu(true);
        _menuDetails.ChangeDetails(tuningDetails);

        _camera.ChangePositionCamera(tuningDetails.CameraPosition, tuningDetails.CameraRotation);
    }

    private void OnChangedCar(CarPlayer car)
    {
        _car = car;

        SetCarTuning();
        _switcherTunings.SetCarTuning(_carTuning);
    }

    private void SetCarTuning()
    {
        foreach (Transform carElements in _car.transform.GetComponentsInChildren<Transform>())
        {
            if (carElements.gameObject.GetComponent<CarTuning>() != null)
                _carTuning = carElements.gameObject.GetComponent<CarTuning>();
        }
    }

    private void OnTryBuyedDetail(TuningDetails _tuningDetails)
    {
        if (_shopMenu.TrySellCar(_tuningDetails.TemporaryTuning.Price) == true)
        {
            _tuningDetails.TemporaryTuning.BuyTuning();
            _tuningDetails.ChangeCurrentTuning();
            _menuDetails.CahngeVisibilityButton();
            _car.SaveBuy();
        }
    }
}