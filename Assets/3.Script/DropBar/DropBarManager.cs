using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBarManager : MonoBehaviour
{
    // 드롭바에 있는 포션을 관리한다. 개수, 종류 포함
    // 개수가 줄었을때 ( 손님에게 파는 경우 : 돈을 번다 (MoneyManager))
    // 개수가 늘었을때 ( CraftRoom에서 생성을 성공했다! )
    // DropHandler, PortionDragHandler 

    public static DropBarManager Instance{ get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    [System.Serializable]
    public class DropbarSlot
    {
        public PortionData portion;
        public int count;
    }

    // 실제 드롭바에 있는 포션들
    public List<DropbarSlot> slots = new List<DropbarSlot>();

    // 슬롯 최대 개수 등 규칙이 있으면 변수추가
    public int maxSlotCount = 100;

    // 포션 추가(CraftRoom에서 생성을 성공시)
    public void AddPotion(PortionData newPotion, int addcOunt = 1)
    {
        // 같은 포션이 있으면 갯수만 추가
        var slot = slots.Find(s => s.portion == newPotion);
        if (slot != null)
        {
            slot.count += addcOunt;
        }
        else
        {
            // 새 슬롯 생성
            if (slots.Count < maxSlotCount)
            {
                slots.Add(new DropbarSlot { portion = newPotion, count = addcOunt });
            }
            else
            {
                Debug.LogWarning("드롭바 슬롯이 가득 찼습니다!");
            }
        }

        UpdateUI();

    }

    // 포션 판매(손님한테 전달 성공)
    public bool SellPotion(PortionData soldPotion, int sellCount = 1)
    {
        var slot = slots.Find(s => s.portion == soldPotion);
        if (slot != null && slot.count >= sellCount)
        {
            slot.count -= sellCount;
            // 돈지급
            MoneyManager.Instance.AddMoney(soldPotion.Price * sellCount);

            // 개수가 0이 되면 슬롯 삭제
            if (slot.count == 0)
                slots.Remove(slot);

            UpdateUI();
            return true;
        }
        else
        {
            Debug.LogWarning("핻강 포션이 부족합니다!");
            return false;
        }
    }
    //드롭바 UI 갱신
    private void UpdateUI()
    {
        //드롭바 슬롯 UI 갱신 코드
    }

}
