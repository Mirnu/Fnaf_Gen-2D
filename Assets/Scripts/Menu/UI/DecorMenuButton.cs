using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecorMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Color")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;

    [Header("Configuration")]
    [SerializeField] private float _colorChangeTime = 0.5f;
    [SerializeField] private float _tickRate = 50;

    private WaitForSeconds _delay;
    private string _startText;

    private TMP_Text _textLabel;

    private void OnDisable()
    {
        _textLabel.color = _startColor;
        _textLabel.text = _startText;
    }

    private void Awake()
    {
        _textLabel = GetComponent<TMP_Text>();
        _textLabel.color = _startColor;

        _startText = _textLabel.text;
        _delay = new WaitForSeconds(_colorChangeTime / _tickRate);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(changeColor(_endColor));
        _textLabel.text = $"> {_startText}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(changeColor(_startColor));
        _textLabel.text = _startText;
    }

    private IEnumerator changeColor(Color color)
    {
        for (int i = 0; i < _tickRate; i++)
        {
            _textLabel.color = Color.Lerp(_textLabel.color, color, i / _tickRate);
            yield return _delay;
        }
    }
}
