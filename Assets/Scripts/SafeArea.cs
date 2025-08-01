using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    [SerializeField] 
    private RectTransform _canvasRect;

    private RectTransform rectTransform;

    /// <summary>
    /// Определение "игровой зоны" на мобильных устройствах
    /// </summary>
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        float widthRatio = _canvasRect.rect.width / Screen.width;
        float heightRatio = _canvasRect.rect.height / Screen.height;

        float offsetTop = (Screen.safeArea.yMax - Screen.height) * heightRatio;
        float offsetBottom = Screen.safeArea.yMin * heightRatio;
        float offsetRight = (Screen.safeArea.xMax - Screen.width) * widthRatio;
        float offsetLeft = Screen.safeArea.xMin * widthRatio;

        rectTransform.offsetMax = new Vector2(offsetRight, offsetTop);
        rectTransform.offsetMin = new Vector2(offsetLeft, offsetBottom);
        CanvasScaler canvasScaler = _canvasRect.GetComponent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(
            canvasScaler.referenceResolution.x, 
            canvasScaler.referenceResolution.y + Mathf.Abs(offsetTop) + Mathf.Abs(offsetBottom)
            );
    }
}
