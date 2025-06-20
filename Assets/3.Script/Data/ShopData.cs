using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Data")]
public class ShopData : ScriptableObject
{
    [Header("기본정보")]
    public string shopName;         
    public Sprite icon;

    [Header("인수")]
    public int tier;                            // 가게 등급
    public int acquisitionPrice;                // 인수 가격

    [Header("운영비 + 성장")]
    public int baseOperationCost;               // 기본 운영비
    public int baseRevenue;                     // 기본 수익

    [Header("옵션")]
    public float reputationModifier;            // 평판 영향도
    public float customerBoost;                 // 방문객 수 보너스 비율
    [TextArea] public string description;       // 가게 설명
}
