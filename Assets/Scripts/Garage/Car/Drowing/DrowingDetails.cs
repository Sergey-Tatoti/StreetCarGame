using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DrowingDetails : MonoBehaviour
{
    [SerializeField] private List<GameObject> _details;

    private float _currentColorR;
    private float _currentColorG;
    private float _currentColorB;
    private float _currentColorM;

    public int CountPieceCar => _details.Count;
    public float CurrentColorR => _currentColorR;
    public float CurrentColorG => _currentColorG;
    public float CurrentColorB => _currentColorB;
    public float CurrentColorM => _currentColorM;

    public void SetCurrentColors()
    {
       _currentColorR = _details[0].GetComponent<Renderer>().material.color.r;
       _currentColorG = _details[0].GetComponent<Renderer>().material.color.g;
       _currentColorB = _details[0].GetComponent<Renderer>().material.color.b;
       _currentColorM = _details[0].GetComponent<Renderer>().material.GetFloat("_Metallic");
    }

    public void SetValueColors(float colorR, float colorG, float colorB, float colorM)
    {
        _currentColorR = colorR;
        _currentColorG = colorG;
        _currentColorB = colorB;
        _currentColorM = colorM;

        ChangeColor(colorR, colorG, colorB, colorM);
    }

    public GameObject GetPieceCar(int index)
    {
        return _details[index];
    }

    public void ChangeColor(float colorR, float colorG, float colorB, float colorM)
    {
        for (int i = 0; i < _details.Count; i++)
        {
            _details[i].GetComponent<Renderer>().material.color = new Color(colorR, colorG, colorB, 0);
            _details[i].GetComponent<Renderer>().material.SetFloat("_Metallic", colorM);
        }
    }
}