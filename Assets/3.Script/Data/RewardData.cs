using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Gold,
    Reputation,
    Recipe,
    CustomerCard,
    Item
}

[CreateAssetMenu(menuName = "Reward Data")]
public class RewardData : ScriptableObject
{
    [Header("기본 정보")]
    public string rewardName;
    public RewardType type;                         // 보상 타입

    [Header("수량 및 참조")]
    public int amount;                              // (보상물의 )수량
    public RecipeData recipe;                       // CustomerCard가 보상일때 참조
    public CustomerData customer;                   // Item 보상 식별지
    public string itemId;

    [Header("조건")]
    [Tooltip("분기별 보상량 곡선")]
    public AnimationCurve scaleByQuarter;
    [Tooltip("조건부 보상용 키")]
    public string conditionKey;

    [Header("설명")]
    [TextArea] public string description;

}
