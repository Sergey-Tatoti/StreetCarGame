using SaveData;
using UnityEngine;

public class SaveColor : Save
{
    private CarDrowingDetails _carDrowing;

    public override void ChangeData(CarPlayer car, int index, CarDatas data)
    {
        SetCharacteristic(car);
        
            _carDrowing.SetValueColorsCar(data.CarDataCollection[index].RcolorCar, data.CarDataCollection[index].GcolorCar,
            data.CarDataCollection[index].BcolorCar, data.CarDataCollection[index].McolorCar);

            _carDrowing.SetValueColorsSaloon(data.CarDataCollection[index].RcolorSaloon, data.CarDataCollection[index].GcolorSaloon,
            data.CarDataCollection[index].BcolorSaloon, data.CarDataCollection[index].McolorSaloon);

            _carDrowing.SetValueColorsWheels(data.CarDataCollection[index].RcolorWheels, data.CarDataCollection[index].GcolorWheels,
            data.CarDataCollection[index].BcolorWheels, data.CarDataCollection[index].McolorWheels);
    }

    public override void SaveData(CarData data)
    {
        data.RcolorCar = _carDrowing.DrowCar.CurrentColorR;
        data.GcolorCar = _carDrowing.DrowCar.CurrentColorG;
        data.BcolorCar = _carDrowing.DrowCar.CurrentColorB;
        data.McolorCar = _carDrowing.DrowCar.CurrentColorM;
        data.RcolorSaloon = _carDrowing.DrowSaloon.CurrentColorR;
        data.GcolorSaloon = _carDrowing.DrowSaloon.CurrentColorG;
        data.BcolorSaloon = _carDrowing.DrowSaloon.CurrentColorB;
        data.McolorSaloon = _carDrowing.DrowSaloon.CurrentColorM;
        data.RcolorWheels = _carDrowing.DrowWheels.CurrentColorR;
        data.GcolorWheels = _carDrowing.DrowWheels.CurrentColorG;
        data.BcolorWheels = _carDrowing.DrowWheels.CurrentColorB;
        data.McolorWheels = _carDrowing.DrowWheels.CurrentColorM;
    }

    public override void SetValues(CarDatas data)
    {
        data.CarDataCollection.Add(new CarData
        {
            RcolorCar = _carDrowing.DrowCar.CurrentColorR,
            GcolorCar = _carDrowing.DrowCar.CurrentColorG,
            BcolorCar = _carDrowing.DrowCar.CurrentColorB,
            McolorCar = _carDrowing.DrowCar.CurrentColorM,
            RcolorSaloon = _carDrowing.DrowSaloon.CurrentColorR,
            GcolorSaloon = _carDrowing.DrowSaloon.CurrentColorG,
            BcolorSaloon = _carDrowing.DrowSaloon.CurrentColorB,
            McolorSaloon = _carDrowing.DrowSaloon.CurrentColorM,
            RcolorWheels = _carDrowing.DrowWheels.CurrentColorR,
            GcolorWheels = _carDrowing.DrowWheels.CurrentColorG,
            BcolorWheels = _carDrowing.DrowWheels.CurrentColorB,
            McolorWheels = _carDrowing.DrowWheels.CurrentColorM,
        });
    }

    public override void SetCharacteristic(CarPlayer car)
    {
        foreach (Transform carElements in car.transform.GetComponentsInChildren<Transform>())
        {
            if (carElements.gameObject.GetComponent<CarDrowingDetails>() != null)
                _carDrowing = carElements.gameObject.GetComponent<CarDrowingDetails>();
        }
    }
}