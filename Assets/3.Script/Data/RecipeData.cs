using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RecipeData")]
public class RecipeData : ScriptableObject
{
    [Header("기본 정보")]
    public string recipeName;
    public Sprite icon;


    [Header("재료 + 순서")]
    public List<IngredientData> ingredients;


    [Header("제작 파라미터")]
    public float baseCraftTime = 2f;
    public int basePrice = 100;
    [Range(0f, 1f)] public float rhythmDifficulty;


    //[Header("추가옵션")]
    //public Rarity rarity;
}
