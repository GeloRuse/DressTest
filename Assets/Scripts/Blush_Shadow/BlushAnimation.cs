using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BlushAnimation : MonoBehaviour
{
    public event Action<int> OnAnimationEnded;

    [SerializeField]
    private RectTransform playerHand;

    [SerializeField]
    private GameObject bookBrushGO;
    [SerializeField]
    private GameObject brushGO;
    [SerializeField]
    private Image brushPickImage;

    [SerializeField]
    private Image blushImage;

    [SerializeField]
    private RectTransform defaultHandPosition;
    [SerializeField]
    private RectTransform pickupPosition;
    [SerializeField]
    private RectTransform afterPickupPosition;

    [SerializeField]
    private RectTransform bodyCheek1Position;
    [SerializeField]
    private RectTransform bodyCheek2Position;

    /// <summary>
    /// Анимация подбора кисти
    /// </summary>
    /// <param name="selectedBlush">выбранная румяна</param>
    public void PickUpBrushAnimation(MakeUpItem selectedBlush)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            bookBrushGO.SetActive(true);
            brushGO.SetActive(false);
            brushPickImage.gameObject.SetActive(false);
            OnAnimationEnded?.Invoke(0);
        });
        seq.Append(playerHand.DOMove(pickupPosition.position, 1f));
        seq.AppendInterval(.1f);
        seq.AppendCallback(() => 
        {
            bookBrushGO.SetActive(false);
            brushGO.SetActive(true);
            PickBlushAnimation(selectedBlush);
        });
    }

    /// <summary>
    /// Анимация выбора румяны
    /// </summary>
    /// <param name="selectedBlush">выбранная румяна</param>
    public void PickBlushAnimation(MakeUpItem selectedBlush)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(playerHand.DOMove(selectedBlush.HandPos.position, 1f));
        for (int i = 0; i < 2; i++)
        {
            seq.Append(playerHand.DOMove(selectedBlush.HandPos.position + Vector3.right * .1f, .1f));
            seq.Append(playerHand.DOMove(selectedBlush.HandPos.position - Vector3.right * .1f, .1f));
        }
        seq.AppendCallback(() =>
        {
            brushPickImage.gameObject.SetActive(true);
            brushPickImage.color = selectedBlush.ItemColor;
        });
        seq.Append(playerHand.DOMove(afterPickupPosition.position, 1f));
        seq.AppendCallback(() => 
        {
            OnAnimationEnded?.Invoke(1);
        });
    }

    /// <summary>
    /// Анимация применения румяны
    /// </summary>
    /// <param name="selectedBlush">выбранная румяна</param>
    public void ApplyBlushAnimation(MakeUpItem selectedBlush)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            bookBrushGO.SetActive(false);
            brushGO.SetActive(true);
            OnAnimationEnded?.Invoke(0);
        });
        for (int i = 0; i < 3; i++)
        {
            seq.Append(playerHand.DOMove(bodyCheek1Position.position, .1f));
            seq.Append(playerHand.DOMove(bodyCheek2Position.position, .1f));
        }
        seq.AppendCallback(() =>
        {
            blushImage.gameObject.SetActive(true);
            brushPickImage.gameObject.SetActive(false);
            blushImage.sprite = selectedBlush.MakeupSprite;
            PutBackAnimation();
        });
    }

    /// <summary>
    /// Анимация возврата кисти
    /// </summary>
    public void PutBackAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(0));
        seq.Append(playerHand.DOMove(pickupPosition.position, 1f));
        seq.AppendCallback(() => 
        {
            bookBrushGO.SetActive(true);
            brushGO.SetActive(false);
        });
        seq.AppendInterval(.1f);
        seq.Append(playerHand.DOMove(defaultHandPosition.position, 1f));
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(2));
    }
}
