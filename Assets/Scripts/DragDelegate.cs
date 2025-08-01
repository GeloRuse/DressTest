using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDelegate : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public delegate void DragEvent(PointerEventData eventDate, int state);
    public event DragEvent OnDragEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData, 1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData, 2);
    }
}
