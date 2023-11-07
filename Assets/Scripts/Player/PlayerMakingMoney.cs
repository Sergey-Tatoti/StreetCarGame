using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMakingMoney : MonoBehaviour
{
    [SerializeField] private RCC_CarControllerV3 _player;
    [SerializeField] private PlayerModifier _moneyModifier;
    [SerializeField] private TMP_Text _textMoney;

    private int _countMoney;

    public int CountMoney => _countMoney;

    private void FixedUpdate()
    {
        _countMoney += Convert.ToInt32(_player.speed * Time.deltaTime * _moneyModifier.MoneyModifier);
        _textMoney.text = _countMoney.ToString();
    }

    public void OnGotMoney(int money)
    {
        _countMoney += money;
    }
}