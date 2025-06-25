using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirManager : MonoBehaviour
{
    public RecipeData recipeData;
    private List<int> stirOrder;
    private int currentOrderIndex = 0;


    void Start()
    {
        stirOrder = recipeData.stirOrder;
    }
    public void OnDragStart(int pointIndex)
    {
        //순서에 맞게 시작해야만
        if (pointIndex == stirOrder[currentOrderIndex])
        {
            Debug.Log($"드래그 시작: {pointIndex}");
        }
        else
        {
            Debug.Log("잘못된 시작!");
            ResetStir();
        }

    }
    public void OnDragEnd(int pointIndex)
    {
        if (currentOrderIndex + 1 < stirOrder.Count && pointIndex == stirOrder[currentOrderIndex + 1])
        {
            Debug.Log($"드래그 성공: {stirOrder[currentOrderIndex]} → {pointIndex}");
            currentOrderIndex++;
            if (currentOrderIndex == stirOrder.Count - 1)
            {
                Debug.Log("전체 성공!");
                ResetStir();
            }
        }
        else
        {
            Debug.Log("드래그 실패 (중간에 잘못된 포인트 혹은 잘못된 순서)");
            ResetStir();
        }
    }
    private void ResetStir()
    {
        currentOrderIndex = 0;
        // 실패 UI 처리
    }
}
