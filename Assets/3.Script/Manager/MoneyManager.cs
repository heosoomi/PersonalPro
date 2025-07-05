using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    public int CurrentMoney { get; private set; } = 0;

    public delegate void OnMoneyChanged(int newMoney);
    public event OnMoneyChanged MoneyChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMoney(); // PlayerPrefs에서 불러와서 CurrentMoney에 세팅!
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveMoney();
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        MoneyChanged?.Invoke(CurrentMoney);
        SaveMoney(); // 추가: 돈 변동마다 바로 저장!
    }
    public bool SpendMoney(int amount)
    {
        if (CurrentMoney >= amount)
        {
            CurrentMoney -= amount;
            MoneyChanged?.Invoke(CurrentMoney);
            SaveMoney(); // 추가!
            return true;
        }
        else
        {
            Debug.Log("돈이 부족합니다");
            return false;
        }
    }
    public void SetMoney(int amount)
    {
        CurrentMoney = amount;
        MoneyChanged?.Invoke(CurrentMoney);
        SaveMoney(); // 추가!
    }

    public void SaveMoney()
    {
        Debug.Log("돈 저장: " + CurrentMoney);
        PlayerPrefs.SetInt("Money", CurrentMoney);
        PlayerPrefs.Save();
    }
    public void LoadMoney()
    {
        int money = PlayerPrefs.GetInt("Money", 100); // 처음 설치시만 100
        Debug.Log("돈 불러오기: " + money);
        SetMoney(money); // SetMoney에서 이벤트+저장 모두 처리
    }
}
