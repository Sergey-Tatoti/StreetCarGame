using SaveData;

public class SaveUpgrade : Save
{
    private CarCountUpgrades _carUpgrade;

    public override void ChangeData(CarPlayer car, int index, CarDatas data)
    {
        car.ChangeCurrentCountUpgrade(data.CarDataCollection[index].CountUpgradeSpeed, data.CarDataCollection[index].CountUpgradeDownForce,
                data.CarDataCollection[index].CountUpgradeBrakeTorque, data.CarDataCollection[index].CountUpgradeHighSpeedSteer);
    }

    public override void SaveData(CarData data)
    {
        data.CountUpgradeSpeed = _carUpgrade.CountUpgradeSpeed;
        data.CountUpgradeDownForce = _carUpgrade.CountUpgradeDownForce;
        data.CountUpgradeBrakeTorque = _carUpgrade.CountUpgradeBrakeTorque;
        data.CountUpgradeHighSpeedSteer = _carUpgrade.CountUpgradeHighSpeedSteer;
    }

    public override void SetValues(CarDatas data)
    {
        data.CarDataCollection.Add(new CarData
        {
            CountUpgradeSpeed = _carUpgrade.CountUpgradeSpeed,
            CountUpgradeDownForce = _carUpgrade.CountUpgradeDownForce,
            CountUpgradeBrakeTorque = _carUpgrade.CountUpgradeBrakeTorque,
            CountUpgradeHighSpeedSteer = _carUpgrade.CountUpgradeHighSpeedSteer,
        });
    }

    public override void SetCharacteristic(CarPlayer car)
    {
        _carUpgrade = car.GetComponent<CarCountUpgrades>();
    }
}