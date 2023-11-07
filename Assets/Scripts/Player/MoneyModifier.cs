using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyModifier : MonoBehaviour
{
    [SerializeField] private float _modifier;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    public float Modifier => _modifier;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}