using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        MoneyManager.Instance.LoadMoney();   
    }

    // string GetSavePath()
    // {
    //     return Path.Combine(Application.persistentDataPath, "save.json");
    // }
    public void Save()
    {
        MoneyManager.Instance.SaveMoney();
    }
    public void Load()
    {
        MoneyManager.Instance.LoadMoney();
    }
}
