using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StirPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int pointIndex;
    public StirManager manager;

    public void OnPointerDown(PointerEventData eventData)
    {
        manager.OnDragStart(pointIndex);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        manager.OnDragEnd(pointIndex);
    } 
}
