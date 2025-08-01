using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField]
    private BookmarkToggle[] bookmarks;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < bookmarks.Length; i++) 
        {
            bookmarks[i].Setup(i);
            bookmarks[i].OnToggle += TabSelect;
        }
    }

    private void TabSelect(int tabIndex)
    {
        for (int i = 0; i < bookmarks.Length; i++) 
        {
            if (i != tabIndex)
            {
                bookmarks[i].EnableBookmark(false);
            }
        }
    }
}
