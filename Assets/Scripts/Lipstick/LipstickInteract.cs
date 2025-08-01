using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LipstickInteract : MonoBehaviour
{
    public event Action<bool> OnSelected;

    [SerializeField]
    private LipstickAnimation animationScript;

    [SerializeField]
    private MakeUpItem[] lipstickItems;

    [SerializeField]
    private HandDrag dragScript;
    [SerializeField]
    private DragDelegate dragDelegate;

    [SerializeField]
    private TriggerDelegate lipstickTrigger;

    private bool onFace;
    private int currentLipstick = -1;
    private bool isBlocked = false;
    public bool SetBlocked(bool blocked) => isBlocked = blocked;

    private void OnEnable()
    {
        lipstickTrigger.OnTriggerEvent += CheckTrigger;
        animationScript.OnAnimationEnded += AnimationStateCheck;
        dragDelegate.OnDragEvent += CheckInteract;
    }

    private void OnDisable()
    {
        lipstickTrigger.OnTriggerEvent -= CheckTrigger;
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
                EnableButtons(false);
                EnableDrag(false);
                break;
            case 1:
                EnableDrag(true);
                break;
            case 2:
                EnableButtons(true);
                currentLipstick = -1;
                OnSelected?.Invoke(false);
                break;
        }
    }

    private void EnableDrag(bool enable)
    {
        dragScript.ResetTarget();
        dragScript.enabled = enable;
    }

    private void EnableButtons(bool enable)
    {
        for (int i = 0; i < lipstickItems.Length; i++)
        {
            lipstickItems[i].GetComponent<Button>().interactable = enable;
        }
    }

    /// <summary>
    /// Подбор помады
    /// </summary>
    /// <param name="lipsticIndex">выбранная помада</param>
    public void LipstickPickUp(int lipsticIndex)
    {
        if (isBlocked)
            return;

        currentLipstick = lipsticIndex;
        animationScript.PickUpLipstickAnimation(lipstickItems[currentLipstick]);
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
    /// Проверка на запуск анимации применения помады
    /// </summary>
    /// <param name="eventDate"></param>
    /// <param name="state">состояние</param>
    private void CheckInteract(PointerEventData eventDate, int state)
    {
        if (state == 2 && onFace)
        {
            animationScript.ApplyLipstickAnimation(lipstickItems[currentLipstick]);
        }
    }
}
