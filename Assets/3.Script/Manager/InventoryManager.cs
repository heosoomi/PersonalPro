using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public Button confirmButton;
    public CraftUIManager craftUIManager; // Inspector에서 연결

    public Image Inventory;
    public List<InventorySlot> allSlots;
    public List<InventorySlot> selectedSlots = new List<InventorySlot>();

    [HideInInspector]public int item = 1;

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);

        //각 슬롯에 콜백 등록
        foreach (var slot in allSlots)
        {
            slot.onClick += SelectSlot;
        }
    }

    //슬롯이 선택 될 떄 마다 호출됨
    public void SelectSlot(InventorySlot slot)
    {
        if (!selectedSlots.Contains(slot))
        {
            selectedSlots.Add(slot);
        }
        else
        {
            selectedSlots.Remove(slot);
        }
        slot.ToggleSelection();
    }

    public void OnConfirm()
    {
        //아무것도 선택 안했으면 바로 제작 단계로
        if (selectedSlots.Count == 0)
        {
            craftUIManager.GoToCraftStep();
            return;
        }

        //선택된 재료 데이터 CraftUIManager에 전달
        var selectedIngredients = new List<IngredientData>();
        foreach (var slot in selectedSlots)
        {
            selectedIngredients.Add(slot.itemData);
        }

        craftUIManager.selectedIngredients = selectedIngredients;

        //애니메이션 순차 재생
        StartCoroutine(PlayAnimSequentially());
    }
    IEnumerator PlayAnimSequentially()
    {
        foreach (var slot in selectedSlots)
        {
            slot.PlayDropAnim();
            yield return new WaitForSeconds(0.45f); // 애니메이션 길이에 맞게 조정
        }

        yield return new WaitForSeconds(0.2f); //마지막 여유시간
        craftUIManager.GoToCraftStep();
        Inventory.gameObject.SetActive(false);
        selectedSlots.Clear();

        foreach (var slot in allSlots)
        {
            slot.ResetSelection();
        }
        selectedSlots.Clear();
    }

    public void Anim()
    {
        Debug.Log("현재 selectedSlots.Count: " + selectedSlots.Count);
        foreach(var slot in selectedSlots) Debug.Log("선택된 슬롯: " + slot.itemData);
        if (selectedSlots.Count == 0)
        {
            Debug.Log("아무것도 업써요");
            return;
        }
        StartCoroutine(PlayAnimAndThenOpenInventory());
    }


    IEnumerator PlayAnimAndThenOpenInventory()
    {
        // 애니메이션 시작!
        craftUIManager.craftAnimator.SetInteger("DropType", item);
        craftUIManager.craftAnimator.SetTrigger("DROP");

        // 0.05초 정도 대기 후 상태 체크 시작 (즉시 바로 체크하면 아직 상태 진입 전일 수 있음)
        yield return new WaitForSeconds(2.6f);

        // 현재 재생 중인 State의 이름 받아오기 (예시: "ItemInTeeth" 등)
        AnimatorStateInfo stateInfo = craftUIManager.craftAnimator.GetCurrentAnimatorStateInfo(0);
        string currentStateName = "";
        if (stateInfo.IsName("ItemInTeeth")) currentStateName = "ItemInTeeth";
        else if (stateInfo.IsName("ItemInHoney")) currentStateName = "ItemInHoney";
        // ... 필요한 모든 State 추가

        // 상태 진입 대기 (실제로 바뀔 때까지)
        while (!craftUIManager.craftAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentStateName))
            yield return null;

        // 애니메이션 재생 완료 대기
        while (craftUIManager.craftAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;

        // 이제 애니메이션이 "정확히 끝난 뒤"!
        Inventory.gameObject.SetActive(true);

        foreach (var slot in allSlots)
        {
            slot.ResetSelection();
        }
        selectedSlots.Clear();
    }

}
