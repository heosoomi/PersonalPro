using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomerData")]
public class CustomerData : ScriptableObject
{
    [Header("기본 정보")]
    public string customerName; //고객이름
    public Sprite icon; // 아이콘

    [Header("선호도 + 행동")]
    public List<PortionData> LikePortion;
    [Range(0, 100)] public float visitProbability;
    public int patience; //인내도 (대기횟수)

    public int paymentThreshold;
    

}
