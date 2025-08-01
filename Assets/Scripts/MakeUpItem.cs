using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUpItem : MonoBehaviour
{
    [SerializeField]
    private RectTransform handPos;
    [SerializeField]
    private Color itemColor;
    [SerializeField]
    private Sprite makeupSprite;

    public RectTransform HandPos { get { return handPos; } }
    public Color ItemColor { get { return itemColor; } }
    public Sprite MakeupSprite { get { return makeupSprite; } }
}
