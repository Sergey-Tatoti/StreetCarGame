using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuning : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField]private bool _isBuyed;

    public int Price => _price;
    public bool IsBuyed => _isBuyed;

    public void BuyTuning()
    {
        _isBuyed = true;
    }
}