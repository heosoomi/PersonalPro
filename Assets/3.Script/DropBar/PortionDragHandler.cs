using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortionDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image portionImage;
    public PortionData PortionData;
    private GameObject dragIcon;
    private RectTransform dragTransform;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(canvas.transform, false);
        dragIcon.transform.SetAsLastSibling();

        Image image = dragIcon.AddComponent<Image>();
        image.sprite = portionImage.sprite;
        image.raycastTarget = false;

        dragTransform = dragIcon.GetComponent<RectTransform>();
        dragTransform.sizeDelta = new Vector2(114, 214); // 사이즈 조절
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragTransform != null)
        {
            dragTransform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragIcon);
    }

    public void ForceEndDrag()
    {
        if (dragIcon != null)
        {
            Destroy(dragIcon);
        }
    }
}
