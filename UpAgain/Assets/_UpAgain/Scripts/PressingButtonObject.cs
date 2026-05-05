using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressingButtonObject : MonoBehaviour
{
    [SerializeField] private ButtonController _buttonController;

    private Graphic _graphic;
    private Color _startedColor;
    private Selectable _selectable;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _graphic = GetComponent<Graphic>();
        _startedColor = _graphic.color;

        _selectable = _buttonController.GetComponent<Selectable>();
    }

    private void OnEnable()
    {
        if (_currentCoroutine != null)
        {
            PointerUp();
        }

        _buttonController.OnClickDown += PointerDown;
        _buttonController.OnClickUp += PointerUp;
    }

    private void OnDisable()
    {
        _buttonController.OnClickDown -= PointerDown;
        _buttonController.OnClickUp -= PointerUp;
    }

    private void PointerDown()
    {
        if (!_selectable.interactable)
            return;

        Color pressedColor = Color.white - _selectable.colors.pressedColor;
        
        StartFade(_startedColor - pressedColor);
    }

    private void PointerUp()
    {
        if (!_selectable.interactable)
            return;
        
        StartFade(_startedColor);
    }

    private void StartFade(Color color)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(Fade(color));
    }

    private IEnumerator Fade(Color newColor)
    {
        Color startColor = _graphic.color;
        float time = 0f;
        float duration = _selectable.colors.fadeDuration;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            _graphic.color = Color.Lerp(startColor, newColor, t);
            yield return null;
        }

        _graphic.color = newColor;
    }
}
