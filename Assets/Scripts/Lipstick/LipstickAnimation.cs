using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LipstickAnimation : MonoBehaviour
{
    public event Action<int> OnAnimationEnded;

    [SerializeField]
    private RectTransform playerHand;

    [SerializeField]
    private Image lipstickHandImage;

    [SerializeField]
    private Image lipstickImage;

    [SerializeField]
    private RectTransform defaultHandPosition;
    [SerializeField]
    private RectTransform afterPickupPosition;

    [SerializeField]
    private RectTransform bodyMouth1Position;
    [SerializeField]
    private RectTransform bodyMouth2Position;

    /// <summary>
    /// Анимация подбора помады
    /// </summary>
    /// <param name="selectedLipstick">выбранная помада</param>
    public void PickUpLipstickAnimation(MakeUpItem selectedLipstick)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            selectedLipstick.gameObject.SetActive(true);
            lipstickHandImage.gameObject.SetActive(false);
            OnAnimationEnded?.Invoke(0);
        });
        seq.Append(playerHand.DOMove(selectedLipstick.HandPos.position, 1f));
        seq.AppendInterval(.5f);
        seq.AppendCallback(() =>
        {
            selectedLipstick.gameObject.SetActive(false);
            lipstickHandImage.gameObject.SetActive(true);
            lipstickHandImage.sprite = selectedLipstick.GetComponent<Image>().sprite;
        });
        seq.Append(playerHand.DOMove(afterPickupPosition.position, 1f));
        seq.AppendCallback(() =>
        {
            OnAnimationEnded?.Invoke(1);
        });
    }

    /// <summary>
    /// Анимация применения помады
    /// </summary>
    /// <param name="selectedLipstick">выбранная помада</param>
    public void ApplyLipstickAnimation(MakeUpItem selectedLipstick)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            selectedLipstick.gameObject.SetActive(false);
            lipstickHandImage.gameObject.SetActive(true);
            OnAnimationEnded?.Invoke(0);
        });
        for (int i = 0; i < 3; i++)
        {
            seq.Append(playerHand.DOMove(bodyMouth1Position.position, .1f));
            seq.Append(playerHand.DOMove(bodyMouth2Position.position, .1f));
        }
        seq.AppendCallback(() =>
        {
            lipstickImage.gameObject.SetActive(true);
            lipstickImage.sprite = selectedLipstick.MakeupSprite;
            PutBackAnimation(selectedLipstick);
        });
    }

    /// <summary>
    /// Анимация возврата помады
    /// </summary>
    /// <param name="selectedLipstick">выбранная помада</param>
    public void PutBackAnimation(MakeUpItem selectedLipstick)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(0));
        seq.Append(playerHand.DOMove(selectedLipstick.HandPos.position, 1f));
        seq.AppendCallback(() => 
        {
            selectedLipstick.gameObject.SetActive(true);
            lipstickHandImage.gameObject.SetActive(false);
        });
        seq.AppendInterval(.1f);
        seq.Append(playerHand.DOMove(defaultHandPosition.position, 1f));
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(2));
    }
}
