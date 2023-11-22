using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    private const string SaveKey = "PlayerSave";

    [SerializeField] private Player _player;

    private void OnEnable() 
    {
        _player.SavedAll += OnSavedAll;
        _player.LoadedAll += OnLoadAll;
    }

    private void OnDisable() 
    {
        _player.SavedAll -= OnSavedAll;
        _player.LoadedAll -= OnLoadAll;
    }

    public void OnSavedAll(int money, int index)
    {
        SaveManager.Save(SaveKey, GetSaveSnapshot(money, index));
    }

    public void OnLoadAll()
    {
        var data = SaveManager.Load<SaveData.PlayerData>(SaveKey);

        _player.SetValueLoading(data.Money, data.IndexCurrentCar);
    }

    SaveData.PlayerData GetSaveSnapshot(int money, int index)
    {
        var data = new SaveData.PlayerData()
        {
            Money = money,
            IndexCurrentCar = index,
        };

        return data;
    }
}