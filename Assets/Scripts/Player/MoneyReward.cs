using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyReward : MonoBehaviour
{
    [SerializeField] private int _bestReward = 200;
    [SerializeField] private float _bestDistance = 2f;
    [SerializeField] private int _normalReward = 100;
    [SerializeField] private float _normalDistance = 2.3f;
    [SerializeField] private int _weekReward = 50;
    [SerializeField] private float _weekDistance = 2.8f;

    public event UnityAction<int> GotMoney;

    public void OnAlmostTochedCar(GameObject anotherCar)
    {
        float currentDistance = transform.transform.position.x - anotherCar.transform.position.x;

        if (TryGetReward(currentDistance, _bestDistance, _bestReward) == false)
        {
            if (TryGetReward(currentDistance, _normalDistance, _normalReward) == false)
            {
                TryGetReward(currentDistance, _weekDistance, _weekReward);
            }
        }
    }

    private bool TryGetReward(float currentDistance, float interval, int reward)
    {
        bool isGetReward = false;

        if (Math.Abs(currentDistance) <= interval)
        {
            isGetReward = true;
            GotMoney?.Invoke(reward);
        }

        return isGetReward;
    }
}