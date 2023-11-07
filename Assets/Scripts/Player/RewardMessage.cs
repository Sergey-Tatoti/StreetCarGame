using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _countMoneyText;

    public void OnGotMoney(int money)
    {
      _countMoneyText.text = money.ToString();

      gameObject.SetActive(true);
      Invoke(nameof(Hide), 0.4f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}