using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private Player _player;

    private void OnEnable() 
    {
        _player.ChangedMoney += OnChangedMoney;
    }

    private void OnDisable() 
    {
        _player.ChangedMoney -= OnChangedMoney;
    }

    private void OnChangedMoney(int money)
    {
       _textMoney.text = money.ToString();
    }
}