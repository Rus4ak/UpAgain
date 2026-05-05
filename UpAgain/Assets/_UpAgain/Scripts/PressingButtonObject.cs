using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressingButtonObject : MonoBehaviour
{
    [SerializeField] private ButtonController _buttonController;

    private Graphic _graphic;
    private Color _startedColor;
    private Selectable _selectable;

    private void Awake()
    {
        _graphic = GetComponent<Graphic>();
        _startedColor = _graphic.color;

        _selectable = _buttonController.GetComponent<Selectable>();
    }

    private void OnEnable()
    {
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
        Color pressedColor = Color.white - _selectable.colors.pressedColor;

        StartCoroutine(Fade(_startedColor - pressedColor));
    }

    private void PointerUp()
    {
        StartCoroutine(Fade(_startedColor));
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
