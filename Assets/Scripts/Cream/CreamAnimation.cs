using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamAnimation : MonoBehaviour
{
    public event Action<int> OnAnimationEnded;

    [SerializeField]
    private RectTransform playerHand;

    [SerializeField]
    private GameObject shelfCreamGO;
    [SerializeField]
    private GameObject handCreamGO;
    [SerializeField]
    private GameObject acneGO;

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
    /// Анимация подбора крема
    /// </summary>
    public void PickUpBottleAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => 
        {
            shelfCreamGO.SetActive(true);
            handCreamGO.SetActive(false);
            OnAnimationEnded?.Invoke(0);
        });
        seq.Append(playerHand.DOMove(pickupPosition.position, 1f));
        seq.AppendInterval(.1f);
        seq.AppendCallback(() => {
            shelfCreamGO.SetActive(false);
            handCreamGO.SetActive(true);
        });
        seq.Append(playerHand.DOMove(afterPickupPosition.position, 1f));
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(1));
    }

    /// <summary>
    /// Анимация применения крема
    /// </summary>
    public void ApplyCreamAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            shelfCreamGO.SetActive(false);
            handCreamGO.SetActive(true);
            OnAnimationEnded?.Invoke(0);
        });
        seq.Append(playerHand.DOMove(bodyCheek1Position.position, 1f));
        seq.AppendInterval(.5f);
        seq.Append(playerHand.DOMove(bodyCheek2Position.position, 1f));
        seq.AppendInterval(.5f);
        seq.AppendCallback(() => 
        { 
            acneGO.SetActive(false);
            PutBackAnimation(); 
        });
    }

    /// <summary>
    /// Анимация возврата бутылки
    /// </summary>
    public void PutBackAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(0));
        seq.Append(playerHand.DOMove(pickupPosition.position, 1f));
        seq.AppendCallback(() => {
            shelfCreamGO.SetActive(true);
            handCreamGO.SetActive(false);
        });
        seq.AppendInterval(.1f);
        seq.Append(playerHand.DOMove(defaultHandPosition.position, 1f));
        seq.AppendCallback(() => OnAnimationEnded?.Invoke(2));
    }
}
