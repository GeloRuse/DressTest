using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerDelegate : MonoBehaviour
{
    public delegate void TriggerEvent(Collider2D collision, bool enter);
    public event TriggerEvent OnTriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEvent?.Invoke(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerEvent?.Invoke(collision, false);
    }
}
