using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    private ScrollRect _scrollRect;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    public void ScrollTo(float value)
    {
        _scrollRect.verticalNormalizedPosition = value;
    }
}
