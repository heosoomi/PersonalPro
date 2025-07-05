using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    
    public int level;

    public PlayerData()
    {
        money = 300;
        level = 1;

    }
}