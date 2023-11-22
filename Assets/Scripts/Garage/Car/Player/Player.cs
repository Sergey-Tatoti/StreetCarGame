using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private List<CarPlayer> _cars;

    private CarPlayer _currentCar;
    private int _indexCurrentCar = 0;
    private int _money = 100000;

    public event UnityAction<int> ChangedMoney;
    public event UnityAction<int, int> SavedAll;
    public event UnityAction LoadedAll;

    public CarPlayer CurrentCar => _currentCar;
    public int CountCars => _cars.Count;
    public int IndexCurrentCar => _indexCurrentCar;
    public int Money => _money;

    private void Start()
    {
        LoadedAll?.Invoke();
        SavedAll?.Invoke(_money, _indexCurrentCar);

        _currentCar = _cars[_indexCurrentCar];
        _currentCar.gameObject.SetActive(true);

        ChangedMoney?.Invoke(_money);
    }

    public void Buy(int price)
    {
        _money -= price;

        SavedAll?.Invoke(_money, _indexCurrentCar);

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

                SavedAll?.Invoke(_money, _indexCurrentCar);
            }
        }
    }

    public void SetValueLoading(int money, int indexCurrentCar)
    {
       _money = money;
       _indexCurrentCar = indexCurrentCar;
    }
}