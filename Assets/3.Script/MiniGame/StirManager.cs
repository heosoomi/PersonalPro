using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StirManager : MonoBehaviour
{
    public RecipeData recipeData;
    private List<int> stirOrder;
    // StirManager 내부
    private int dragStartIndex = -1;
    private int currentOrderIndex = 0;

    public List<StirPoint> stirPoints;
    private StirPoint prevHoverPoint;
   
    void Start()
    {
        stirOrder = new List<int>(recipeData.stirOrder ?? new List<int>());

    }

    void Update()
    {
        //드래그 중일때만 체크
        if (Input.GetMouseButton(0))
        {
            PointerEventData ped = new PointerEventData(EventSystem.current);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            StirPoint hoverPoint = null;
            foreach (var r in results)
            {
                hoverPoint = r.gameObject.GetComponent<StirPoint>();
                if (hoverPoint != null) break;
            }

            //이전 포인트 Hightlight 해제
            if (prevHoverPoint != null && prevHoverPoint != hoverPoint)
                prevHoverPoint.SetHighlight(false);

            //현재 포인트 Highlight 적용
            if (hoverPoint != null)
                hoverPoint.SetHighlight(true);

            prevHoverPoint = hoverPoint;
        }
        else
        {
            //드래그가 끝나면 전체 하이라이트 해제
            if (prevHoverPoint != null)
            {
                prevHoverPoint.SetHighlight(false);
                prevHoverPoint = null;
            }
        }

    }

    public void OnDragStart(int pointIndex)
    {
        dragStartIndex = pointIndex;
    }

    public void OnDragEnd(int pointIndex)
    {
        if (stirOrder == null || stirOrder.Count < 2) return;

        int expectedStart = stirOrder[currentOrderIndex];
        int expectedEnd = stirOrder[currentOrderIndex + 1];

        Debug.Log($"expectedStart: {expectedStart}, expectedEnd: {expectedEnd}");

        if (dragStartIndex == expectedStart && pointIndex == expectedEnd)
        {
            Debug.Log($"드래그 성공: {expectedStart} → {expectedEnd}");
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
        dragStartIndex = -1;
    }
    private void ResetStir()
    {
        currentOrderIndex = 0;
        dragStartIndex = -1;
        // 실패 UI 처리 등
    }
   

}