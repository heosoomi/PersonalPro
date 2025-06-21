using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그 된 아이템 가져오기
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            Debug.Log("손님에게 포션이 전달 됌!");

            //**소비처리 포션 적용 로직
        }
    }
}
