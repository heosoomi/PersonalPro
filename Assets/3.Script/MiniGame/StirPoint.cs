using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StirPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int pointIndex;
    public StirManager manager;
   // public DragLineDrawer dragLineDrawer;

    public void OnPointerDown(PointerEventData eventData)
    {
        //dragLineDrawer.StartDrag(eventData.position);
        manager.OnDragStart(pointIndex);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //dragLineDrawer.DragTo(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("UP " + pointIndex);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        int upPointIndex = pointIndex; // fallback

        foreach (var r in results)
        {
            StirPoint sp = r.gameObject.GetComponent<StirPoint>();
            if (sp != null)
            {
                upPointIndex = sp.pointIndex;
                break;
            }
        }

        manager.OnDragEnd(upPointIndex);
        //dragLineDrawer.EndDrag();
    }
    public void SetHighlight(bool isOn)
    {
        var img = GetComponent<UnityEngine.UI.Image>();
        if (img != null)
        {
            img.color = isOn ? new Color(1f, 0.9f, 0.6f, 1f) : Color.white;
        }
        transform.localScale = isOn ? Vector3.one * 1.15f : Vector3.one;
        
    }
}

