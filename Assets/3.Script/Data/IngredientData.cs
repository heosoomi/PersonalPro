using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Ingredient Data")]
public class IngredientData : ScriptableObject
{
    [Header("기본정보")]
    public string ingredientName;   // 재료이름
    public Sprite icon;             // UI 아이콘
    public Rarity rarity;           // 희귀도

    [Header("속성")]
    public int basePrice = 10;          // 기본구매가
    //public float spawnProbability;    // 모험시 획득 확률


    [Header("옵션")]
    [TextArea] public string description;       // 재료설명
}
