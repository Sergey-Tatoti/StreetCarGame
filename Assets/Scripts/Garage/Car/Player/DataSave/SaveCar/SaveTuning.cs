using System.Collections.Generic;
using SaveData;
using UnityEngine;

public class SaveTuning : Save
{
    private CarTuning _carTuning;

    public override void ChangeData(CarPlayer car, int index, CarDatas data)
    {
        SetCharacteristic(car);
        
         _carTuning.SetCurrentTunings(data.CarDataCollection[index].IndexesTuningRBump,
                data.CarDataCollection[index].IndexesTuningFBump, data.CarDataCollection[index].IndexesTuningSpoiler,
                data.CarDataCollection[index].IndexesTuningSkid, data.CarDataCollection[index].IndexesTuningHood);
    }

    public override void SaveData(CarData data)
    {
        TryAddIndexListTuning(data.IndexesTuningRBump, _carTuning.RBump.CurrentIndexTuning);
        TryAddIndexListTuning(data.IndexesTuningFBump, _carTuning.FBump.CurrentIndexTuning);
        TryAddIndexListTuning(data.IndexesTuningSpoiler, _carTuning.Spoiler.CurrentIndexTuning);
        TryAddIndexListTuning(data.IndexesTuningSkid, _carTuning.Skid.CurrentIndexTuning);
        TryAddIndexListTuning(data.IndexesTuningHood, _carTuning.Hood.CurrentIndexTuning);
    }

    public override void SetValues(CarDatas data)
    {
        data.CarDataCollection.Add(new CarData
        {
            IndexesTuningRBump = new List<int>() { _carTuning.RBump.CurrentIndexTuning },
            IndexesTuningFBump = new List<int>() { _carTuning.FBump.CurrentIndexTuning },
            IndexesTuningSpoiler = new List<int>() { _carTuning.Spoiler.CurrentIndexTuning },
            IndexesTuningSkid = new List<int>() { _carTuning.Skid.CurrentIndexTuning },
            IndexesTuningHood = new List<int>() { _carTuning.Hood.CurrentIndexTuning },
        });
    }

    public override void SetCharacteristic(CarPlayer car)
    {
        foreach (Transform carElements in car.transform.GetComponentsInChildren<Transform>())
        {
            if (carElements.gameObject.GetComponent<CarTuning>() != null)
                _carTuning = carElements.gameObject.GetComponent<CarTuning>();
        }
    }

    private void TryAddIndexListTuning(List<int> tunings, int index)
    {
        bool haveIndex = false;

        for (int i = 0; i < tunings.Count; i++)
        {
            if (tunings[i] == index)
            {
                haveIndex = true;
                break;
            }
        }

        if (haveIndex == false)
            tunings.Add(index);
    }
}