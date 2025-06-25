using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public Customer currentCustomer; // 드롭을 받을 손님

    [SerializeField] private Transform dropTransform; // Horizontal Layout이 붙은 부모
    [SerializeField] private GameObject portionSlotPrefab; //슬롯프리팹

    public void SetCustomer(Customer customer)
    {
        currentCustomer = customer;
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 된 아이템 가져오기
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            Debug.Log("포션 드롭 성공" + dropped.name);

            // 드롭된 포션 슬롯에서 ReceipeData 정보 가져오기  
            PortionSlotData slotData = dropped.GetComponent<PortionSlotData>();
            if (slotData == null)
            {
                Debug.LogWarning("드롭된 포션에 PortionSlotData 컴포넌트가 없음!");
                return;
            }
            if (slotData.recipeData == null)
            {
                Debug.LogWarning("드롭된 포션에 recipeData가 비어있음!");
                return;
            }
            RecipeData droppedRecipe = slotData.recipeData;

            // 손님에게 포션 시도
            if (currentCustomer != null)
            {
                bool success = currentCustomer.TryReceivePortion(droppedRecipe);

                if (success)
                {
                    Debug.Log($"손님이 포션을 만족해함!");
                    slotData.UsePortion(); // 수량 1 감소

                    // 드래그 이미지 수동 제거
                    PortionDragHandler dragHandler = dropped.GetComponent<PortionDragHandler>();
                    if (dragHandler != null)
                    {
                        dragHandler.ForceEndDrag();
                    }
                }
                else
                {
                    Debug.Log("손님이 이 포션을 원하지 않음!");
                }
            }

        }
    }
    public void AddPortionSlot(RecipeData recipeToAssign)
    {
        // 슬롯 생성
        GameObject slot = Instantiate(portionSlotPrefab, dropTransform);

        // 슬롯 내부 스크립트에 레시피 연결
        PortionSlotData data = slot.GetComponent<PortionSlotData>();
        data.recipeData = recipeToAssign;
        data.count = 1; // 수량 초기 설정등도 여기서 가능

        // 아이콘이나 텍스트 업데이트 함수도 추가 가능
        data.UpdateCountUI();
    }
}
