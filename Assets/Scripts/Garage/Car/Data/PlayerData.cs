
namespace SaveData
{
    [System.Serializable]
    public class PlayerData
    {
      public int IndexCurrentCar;
      public int Money;

      public PlayerData()
      {
        IndexCurrentCar = 0;
        Money = 10000;
      }
    }
}