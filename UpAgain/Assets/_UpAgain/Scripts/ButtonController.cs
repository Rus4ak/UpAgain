using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnClickDown;
    public event Action OnClickUp;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickUp?.Invoke();
    }
}
