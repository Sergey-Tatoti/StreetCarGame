
using System.Collections.Generic;

namespace SaveData
{
    [System.Serializable]
    public class CarData
    {
        public int Id;
        public bool IsBuyed;
        public int CountUpgradeSpeed;
        public int CountUpgradeDownForce;
        public int CountUpgradeBrakeTorque;
        public int CountUpgradeHighSpeedSteer;
        public List<int> IndexesTuningRBump;
        public List<int> IndexesTuningFBump;
        public List<int> IndexesTuningSpoiler;
        public List<int> IndexesTuningSkid;
        public List<int> IndexesTuningHood;
        public float RcolorCar = 0;
        public float GcolorCar = 0;
        public float BcolorCar = 0;
        public float McolorCar = 0;
        public float RcolorSaloon = 0;
        public float GcolorSaloon = 0;
        public float BcolorSaloon = 0;
        public float McolorSaloon = 0;
        public float RcolorWheels = 0;
        public float GcolorWheels = 0;
        public float BcolorWheels = 0;
        public float McolorWheels = 0;
    }
}