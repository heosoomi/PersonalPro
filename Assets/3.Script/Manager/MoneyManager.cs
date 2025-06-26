using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int CurrentMoney { get; private set; } = 0;

    //돈 변동시 UI에 반영하고 싶으면 이벤트 사용 가능
    public delegate void OnMoneyChanged(int newMoney);
    public event OnMoneyChanged MoneyChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        MoneyChanged?.Invoke(CurrentMoney);
    }
    public bool SpendMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            MoneyChanged?.Invoke(CurrentMoney);
            return true;
        }
        else
        {
            //돈부족시 처리
            Debug.Log("돈이 부족합니다");
            return false;
        }
    }
    public void SetMoney(int amount)
    {
        CurrentMoney = amount;
        MoneyChanged?.Invoke(CurrentMoney);
    }
    // 판매함수 예시
    public void SellPotion(int PotionPrice)
    {
        MoneyManager.Instance.AddMoney(PotionPrice);
        //기타 판매 처리(인벤토리 제거 등)
    }
}
