using System.Collections;
using System.Collections.Generic;
using SaveData;
using UnityEngine;

public class SaveCar : MonoBehaviour
{
    private const string SaveKey = "CarSave";

    [SerializeField] private List<CarPlayer> _carPlayers;
    [SerializeField] private Save _saveUpgrade;
    [SerializeField] private Save _saveTuning;
    [SerializeField] private Save _saveColor;

    private SaveData.CarDatas _data;
    private CarPlayer _currentCar;
    private int _id;
    private bool _isBuyed;

    public bool IsBuyed => _isBuyed;

    private void OnEnable()
    {
        for (int i = 0; i < _carPlayers.Count; i++)
        {
            _carPlayers[i].Saved += SavedAll;
            _carPlayers[i].Loaded += LoadAll;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _carPlayers.Count; i++)
        {
            _carPlayers[i].Saved -= SavedAll;
            _carPlayers[i].Loaded -= LoadAll;
        }
    }

    public void SavedAll(int id)
    {
        _id = id;
        SaveManager.Save(SaveKey, GetSaveSnapshot());
    }

    public void LoadAll(int id)
    {
        _id = id;
        _data = SaveManager.Load<SaveData.CarDatas>(SaveKey);

        ChangeDatas();
    }

    private void ChangeDatas()
    {
        for (int i = 0; i < _data.CarDataCollection.Count; i++)
        {
            if (_data.CarDataCollection[i].Id == _id)
            {
                GetCurrentCar().ChangeData(_data.CarDataCollection[i].IsBuyed);
                _saveUpgrade.ChangeData(GetCurrentCar(), i, _data);
                _saveTuning.ChangeData(GetCurrentCar(), i, _data);
                _saveColor.ChangeData(GetCurrentCar(), i, _data);

                break;
            }
        }
    }

    private SaveData.CarDatas GetSaveSnapshot()
    {
        _data = SaveManager.Load<SaveData.CarDatas>(SaveKey);

        _saveUpgrade.SetCharacteristic(GetCurrentCar());
        _saveTuning.SetCharacteristic(GetCurrentCar());
        _saveColor.SetCharacteristic(GetCurrentCar());

        if (TrySaveDatas() == false)
        {
            CreateNewData();
        }

        return _data;
    }

    private bool TrySaveDatas()
    {
        for (int i = 0; i < _data.CarDataCollection.Count; i++)
        {
            if (_data.CarDataCollection[i].Id == _id)
            {
                _data.CarDataCollection[i].IsBuyed = GetCurrentCar().IsBuyed;
                _saveUpgrade.SaveData(_data.CarDataCollection[i]);
                _saveTuning.SaveData(_data.CarDataCollection[i]);
                _saveColor.SaveData(_data.CarDataCollection[i]);

                return true;
            }
        }

        return false;
    }

    private void CreateNewData()
    {
        _saveUpgrade.SetValues(_data);
        _saveTuning.SetValues(_data);
        _saveColor.SetValues(_data);

        _data.CarDataCollection.Add(new CarData
        {
            Id = _id,
            IsBuyed = GetCurrentCar().IsBuyed,
        });
    }

    private CarPlayer GetCurrentCar()
    {
        for (int i = 0; i < _carPlayers.Count; i++)
        {
            if (_carPlayers[i].Id == _id)
                _currentCar = _carPlayers[i];
        }

        return _currentCar;
    }
}