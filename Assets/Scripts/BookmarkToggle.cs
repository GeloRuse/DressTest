using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarkToggle : MonoBehaviour
{
    public event Action<int> OnToggle;

    [SerializeField]
    private GameObject tab;

    [SerializeField]
    private GameObject unselected;
    [SerializeField]
    private GameObject selected;

    private int tabIndex;
    private bool active;

    public void Setup(int index)
    {
        tabIndex = index;
    }

    public void ToggleBookmark()
    {
        if (active) 
        {
            EnableBookmark(false);
        }
        else
        {
            EnableBookmark(true);
            OnToggle?.Invoke(tabIndex);
        }
        active = !active;
    }

    public void EnableBookmark(bool enable)
    {
        tab.SetActive(enable);
        unselected.SetActive(!enable);
        selected.SetActive(enable);

        active = enable;
    }
}
