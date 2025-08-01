using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreamInteract : MonoBehaviour
{
    public event Action<bool> OnSelected;

    [SerializeField]
    private CreamAnimation animationScript;

    [SerializeField]
    private HandDrag dragScript;
    [SerializeField]
    private DragDelegate dragDelegate;
    [SerializeField]
    private Button bottleButton;

    [SerializeField]
    private TriggerDelegate bottleTrigger;

    private bool onFace;
    private bool isBlocked = false;
    public bool SetBlocked(bool blocked) => isBlocked = blocked;

    private void OnEnable()
    {
        bottleTrigger.OnTriggerEvent += CheckTrigger;
        animationScript.OnAnimationEnded += AnimationStateCheck;
        dragDelegate.OnDragEvent += CheckInteract;
    }

    private void OnDisable()
    {
        bottleTrigger.OnTriggerEvent -= CheckTrigger;
        animationScript.OnAnimationEnded -= AnimationStateCheck;
        dragDelegate.OnDragEvent -= CheckInteract;
    }

    /// <summary>
    /// Переключение между состояниями инструмента
    /// </summary>
    /// <param name="state">текущее состояние</param>
    private void AnimationStateCheck(int state)
    {
        switch (state) 
        {
            case 0:
                OnSelected?.Invoke(true);
                EnableButton(false);
                EnableDrag(false);
                break;
            case 1:
                EnableDrag(true);
                break;
            case 2:
                EnableButton(true);
                OnSelected?.Invoke(false);
                break;
        }
    }

    private void EnableDrag(bool enable)
    {
        dragScript.ResetTarget();
        dragScript.enabled = enable;
    }

    private void EnableButton(bool enable)
    {
        bottleButton.interactable = enable;
    }

    /// <summary>
    /// Подбор крема
    /// </summary>
    public void BottlePickUp()
    {
        if (isBlocked)
            return;

        animationScript.PickUpBottleAnimation();
    }

    /// <summary>
    /// Проверка на коллизию с лицом
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="enter">попадание</param>
    private void CheckTrigger(Collider2D collision, bool enter)
    {
        if (collision.gameObject.CompareTag("Face")) 
        {
            onFace = enter;
        }
    }

    /// <summary>
    /// Проверка на запуск анимации применения крема
    /// </summary>
    /// <param name="eventDate"></param>
    /// <param name="state">состояние</param>
    private void CheckInteract(PointerEventData eventDate, int state)
    {
        if(state == 2 && onFace)
        {
            animationScript.ApplyCreamAnimation();
        }
    }
}
