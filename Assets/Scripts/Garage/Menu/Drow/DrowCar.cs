using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowCar : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pieceCar;

    private float _currentColorR;
    private float _currentColorG;
    private float _currentColorB;
    private float _currentColorM;

    public int CountPieceCar => _pieceCar.Count;
    public float CurrentColorR => _currentColorR;
    public float CurrentColorG => _currentColorG;
    public float CurrentColorB => _currentColorB;
    public float CurrentColorM => _currentColorM;

    public void SetCurrentColors()
    {
       _currentColorR = _pieceCar[0].GetComponent<Renderer>().material.color.r;
       _currentColorG = _pieceCar[0].GetComponent<Renderer>().material.color.g;
       _currentColorB = _pieceCar[0].GetComponent<Renderer>().material.color.b;
       _currentColorM = _pieceCar[0].GetComponent<Renderer>().material.GetFloat("_Metallic");
    }

    public GameObject GetPieceCar(int index)
    {
        return _pieceCar[index];
    }

    public void SetValueColors(float colorR, float colorG, float colorB, float colorM)
    {
        _currentColorR = colorR;
        _currentColorG = colorG;
        _currentColorB = colorB;
        _currentColorM = colorM;
    }
}