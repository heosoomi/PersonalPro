using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour
{
    public Animator slotAnimator;
    public IngredientData itemData;
    public Image image;

    public Image outLineImage;
    public Image overlayImage;

    public delegate void ClickEvent(InventorySlot slot);
    public ClickEvent onClick;

    private bool isSelected = false;

    private void Awake()
    {
        if (image == null) return;

        if (itemData != null)
            image.sprite = itemData.icon;

        if (outLineImage != null)
            outLineImage.gameObject.SetActive(false);
        if (overlayImage != null)
            overlayImage.gameObject.SetActive(false);
    }
    public void OnClickSlot()
    {
        onClick?.Invoke(this);
        // 선택된 상태 표시(하이라이트 등)


    }
    public void ToggleSelection()
    {
        isSelected = !isSelected;
        //하이라이트 이미지 온/오프

        if (outLineImage != null)
            outLineImage.gameObject.SetActive(isSelected);
        if (overlayImage != null)
            overlayImage.gameObject.SetActive(isSelected);

    }

    public void PlayDropAnim()
    {
        if (slotAnimator != null && itemData != null)
        {
            slotAnimator.SetInteger("DropType", itemData.dropType);
            slotAnimator.SetTrigger("DROP");

        }
    }

    public void ResetSelection()
    {
        isSelected = false;
        if (outLineImage != null)
            outLineImage.gameObject.SetActive(false);
        if (overlayImage != null)
            overlayImage.gameObject.SetActive(false);
    }

    
}
