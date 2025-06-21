using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Datas/Portion Data")]
public class PortionData : ScriptableObject
{
    [Header("기본정보")]
    public string PortionName;   // 이름
    public Sprite icon;          // UI 아이콘


    [Header("속성")]
    public int Price;          // 기본구매가


    [Header("옵션")]
    [TextArea] public string description;       // 재료설명
}
