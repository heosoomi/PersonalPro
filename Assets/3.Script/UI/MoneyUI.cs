using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        MoneyManager.Instance.MoneyChanged += UpdateMoneyUI;
        UpdateMoneyUI(MoneyManager.Instance.CurrentMoney);
        
    }
    void UpdateMoneyUI(int newMoney)
    {
        Debug.Log("UI 갱신: " + newMoney);
        moneyText.text = $"{newMoney:N0}G";
    }

    private void OnDestroy()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.MoneyChanged -= UpdateMoneyUI;
    }
}
