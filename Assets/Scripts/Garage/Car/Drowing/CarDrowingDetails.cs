using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrowingDetails : MonoBehaviour
{
    [SerializeField] private DrowingDetails _drowCar;
    [SerializeField] private DrowingDetails _drowSaloon;
    [SerializeField] private DrowingDetails _drowWheels;

    public DrowingDetails DrowCar => _drowCar;
    public DrowingDetails DrowSaloon => _drowSaloon;
    public DrowingDetails DrowWheels => _drowWheels;

    public void SetCurrentColors()
    {
        _drowCar.SetCurrentColors();
        _drowSaloon.SetCurrentColors();
        _drowWheels.SetCurrentColors();
    }

    public void SetValueColorsCar(float valueR, float valueG, float valueB, float valueM)
    {
        _drowCar.SetValueColors(valueR, valueG, valueB, valueM);
    }

    public void SetValueColorsSaloon(float valueR, float valueG, float valueB, float valueM)
    {
        _drowSaloon.SetValueColors(valueR, valueG, valueB, valueM);
    }

    public void SetValueColorsWheels(float valueR, float valueG, float valueB, float valueM)
    {
        _drowWheels.SetValueColors(valueR, valueG, valueB, valueM);
    }
}
