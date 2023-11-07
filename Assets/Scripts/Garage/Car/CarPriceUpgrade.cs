using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPriceUpgrade : MonoBehaviour
{
  private const string Speed = "Speed";
  private const string DownForce = "DownForce";
  private const string BrakeTorque = "BrakeTorque";
  private const string HighSpeedSteer = "HighSpeedSteer";

  [SerializeField] private List<int> _pricesSpeedUpgrade;
  [SerializeField] private List<int> _pricesDownForceUpgrade;
  [SerializeField] private List<int> _pricesBrakeTorque;
  [SerializeField] private List<int> _pricesHighSpeedSteer;

  public int GetPriceUpgrade(int index, string characteristic)
  {
    int price = 0;

    if (characteristic == Speed)
      price = _pricesSpeedUpgrade[index];
    else if (characteristic == DownForce)
      price = _pricesDownForceUpgrade[index];
    else if (characteristic == BrakeTorque)
      price = _pricesBrakeTorque[index];
    else if (characteristic == HighSpeedSteer)
      price = _pricesHighSpeedSteer[index];
    else
      Debug.Log("Ошибка в указании константы параметра цены CarPriceUpgrade");

    return price;
  }

  public void FillListPrices(string characteristic, int countUpgrade)
  {
    if (characteristic == Speed && _pricesSpeedUpgrade.Count == 0)
      AddPrices(countUpgrade, _pricesSpeedUpgrade);
    else if (characteristic == DownForce && _pricesDownForceUpgrade.Count == 0)
      AddPrices(countUpgrade, _pricesDownForceUpgrade);
    else if (characteristic == BrakeTorque && _pricesBrakeTorque.Count == 0)
      AddPrices(countUpgrade, _pricesBrakeTorque);
    else if (characteristic == HighSpeedSteer && _pricesHighSpeedSteer.Count == 0)
      AddPrices(countUpgrade, _pricesHighSpeedSteer);
  }

  private void AddPrices(int countUpgrade, List<int> prices)
  {
    for (int i = 0; i < countUpgrade; i++)
    {
      prices.Add(10);
    }
  }
}