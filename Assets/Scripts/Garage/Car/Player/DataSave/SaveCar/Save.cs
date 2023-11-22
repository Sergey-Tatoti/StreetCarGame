using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Save : MonoBehaviour
{
    public virtual void ChangeData(CarPlayer car, int index, SaveData.CarDatas data) {}

    public virtual void SaveData(SaveData.CarData data) {}

    public virtual void SetValues(SaveData.CarDatas data) {}

    public virtual void SetCharacteristic(CarPlayer car) {}
}
