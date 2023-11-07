using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitcherCar : MonoBehaviour
{
    [SerializeField] private Button _buttonArrowRight;
    [SerializeField] private Button _buttonArrowLeft;

    private int _currentIndexCar;
    private int _countCars;
    private int _nextCar = 1;

    public event UnityAction<int, int> SwitchedCar;

    private void OnEnable()
    {
        _buttonArrowRight.onClick.AddListener(() => OnClickButtonArrow(true)); ;
        _buttonArrowLeft.onClick.AddListener(() => OnClickButtonArrow(false)); ;
    }

    private void OnDisable()
    {
        _buttonArrowRight.onClick.RemoveListener(() => OnClickButtonArrow(true)); ;
        _buttonArrowLeft.onClick.RemoveListener(() => OnClickButtonArrow(false)); ;
    }

    public void SetValueCars(int currentIndexCar, int countCars)
    {
        _currentIndexCar = currentIndexCar;
        _countCars = countCars;

        TryChangeActiveButtons();
    }

    private void TryChangeActiveButtons()
    {
        if (_currentIndexCar + _nextCar >= _countCars)
            _buttonArrowRight.interactable = false;
        else
            _buttonArrowRight.interactable = true;

        if (_currentIndexCar - _nextCar < 0)
            _buttonArrowLeft.interactable = false;
        else
            _buttonArrowLeft.interactable = true;
    }

    private void OnClickButtonArrow(bool isRight)
    {
        if (isRight == true)
            SwitchCar(_nextCar);
        else
            SwitchCar(-_nextCar);
    }

    private void SwitchCar(int direction)
    {
        _currentIndexCar += direction;
        TryChangeActiveButtons();

        SwitchedCar?.Invoke(_currentIndexCar, direction);
    }
}