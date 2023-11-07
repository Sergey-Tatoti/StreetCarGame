using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameView : MonoBehaviour
{
    [SerializeField] private PlayerMakingMoney _playerMoney;
    [SerializeField] private PlayerModifier _playerModifier;
    [SerializeField] private PlayerTouchTracker _playerTouchTracker;
    [SerializeField] private MoneyReward _moneyReward;
    [SerializeField] private RewardMessage _rewardMessage;

    private void OnEnable() 
    {
        _playerTouchTracker.AlmostTouchedCar += _moneyReward.OnAlmostTochedCar;
        _playerTouchTracker.TouchedOppositeRoad += _playerModifier.OnTouchedOppositeRoad;
        _playerTouchTracker.TouchedModifier += _playerModifier.OnTouchedModifier;
        _moneyReward.GotMoney += _playerMoney.OnGotMoney;
        _moneyReward.GotMoney += _rewardMessage.OnGotMoney;
    }

    private void OnDisable() 
    {
        _playerTouchTracker.AlmostTouchedCar -= _moneyReward.OnAlmostTochedCar;
        _playerTouchTracker.TouchedOppositeRoad -= _playerModifier.OnTouchedOppositeRoad;
        _playerTouchTracker.TouchedModifier -= _playerModifier.OnTouchedModifier;
        _moneyReward.GotMoney -= _playerMoney.OnGotMoney;
    }
}
