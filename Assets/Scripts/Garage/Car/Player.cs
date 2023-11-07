using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private List<CarPlayer> _cars;

    private CarPlayer _currentCar;
    private int _indexCurrentCar = 0;
    private int _money = 10000;

    public event UnityAction<int> ChangedMoney;

    public CarPlayer CurrentCar => _currentCar;
    public int CountCars => _cars.Count;
    public int IndexCurrentCar => _indexCurrentCar;
    public int Money => _money;

    private void Start()
    {
        if (_currentCar == null)
            _currentCar = _cars[0];

        _currentCar.gameObject.SetActive(true);

        ChangedMoney?.Invoke(_money);
    }

    public void Buy(int price)
    {
        _money -= price;

        ChangedMoney?.Invoke(_money);
    }

    public void ShowCar(int index, int direction)
    {
        _cars[index - direction].gameObject.SetActive(false);
        _cars[index].gameObject.SetActive(true);
    }

    public CarPlayer GetCar(int index)
    {
        return _cars[index];
    }

    public void ChangeCar(CarPlayer car)
    {
        for (int i = 0; i < _cars.Count; i++)
        {
            if (_cars[i] == car)
            {
                _currentCar = car;
                _indexCurrentCar = i;
            }
        }
    }
}