using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, "save.json");
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(playerData, true); //true면 예쁘게 저장됨
        File.WriteAllText(GetSavePath(), json);
        Debug.Log("저장완료! 경로 :" + GetSavePath());
    }
    public void Load()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("불러오기 성공 돈 :" + playerData.gold);
        }
        else
        {
            Debug.Log("세이브 파일 없음. 새로 생성됨!");
            playerData = new PlayerData(); //기본 값 생성
        }
    }
}
