using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandDrag : MonoBehaviour
{
    [SerializeField]
    private DragDelegate handDragHandler;
    [SerializeField]
    private RectTransform handRectTransform;

    private Vector2 localPoint;
    private Vector3 targetPosition;
    private float speed = 10f;

    private void OnEnable()
    {
        handDragHandler.OnDragEvent += HandleDrag;
    }

    private void OnDisable()
    {
        handDragHandler.OnDragEvent -= HandleDrag;
    }

    public void ResetTarget()
    {
        targetPosition = Vector3.zero;
    }

    private void HandleDrag(PointerEventData eventData, int state)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(handRectTransform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPoint);
        targetPosition = localPoint;
    }

    private void Update()
    {
        if (targetPosition != Vector3.zero && Vector3.Distance(handRectTransform.anchoredPosition, targetPosition) > 0.1f)
        {
            handRectTransform.anchoredPosition = Vector3.Lerp(handRectTransform.anchoredPosition, targetPosition, Time.deltaTime * speed);
        }
    }
}
