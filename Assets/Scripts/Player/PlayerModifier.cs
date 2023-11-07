using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerModifier : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMoneyModifier;
    [SerializeField] private float _moneyModifier = 1.1f;
    [SerializeField] private float _modifierChangeLineRoad = 0.2f;

    public float MoneyModifier => _moneyModifier;
    public float ModifierChangeLineRoad => _modifierChangeLineRoad;

    private void Start()
    {
        _textMoneyModifier.text = _moneyModifier.ToString();
    }

    public void OnTouchedModifier(float modifier)
    {
        ChangeMoneyModifier(modifier);
    }

    public void OnTouchedOppositeRoad(bool isOpposite)
    {
        if (isOpposite == true)
            IncreaseModifier(_modifierChangeLineRoad);
        else
            IncreaseModifier(-_modifierChangeLineRoad);
    }

    private void ChangeMoneyModifier(float value)
    {
        _moneyModifier = value;
        _textMoneyModifier.text = _moneyModifier.ToString();
    }

    private void IncreaseModifier(float value)
    {
        _moneyModifier += value;
        _textMoneyModifier.text = _moneyModifier.ToString();
    }
}