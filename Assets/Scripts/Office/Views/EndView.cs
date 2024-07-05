using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject AM5;
    [SerializeField] private GameObject AM6;

    [Header("Configuration")]
    [SerializeField] private float _deltaY = 9.5f;
    [SerializeField] private float _timeTextChanged = 6;
    [SerializeField] private float _timeEnd = 11;

    private LevelController _levelController => LevelController.Instance;
    private AudioSource _audioSource;

    private WaitForSeconds _deltaChangeTimeText;
    private int _ticks = 100;

    private void Awake()
    {
        _deltaChangeTimeText = new WaitForSeconds(_timeTextChanged / _ticks);
        _audioSource = GetComponent<AudioSource>();
    }

    public void StartEnd()
    {
        _audioSource.Play();
        StartCoroutine(startChange());
    }

    private IEnumerator startChange()
    {
        for (int i = 0; i < _ticks; i++)
        {
            yield return _deltaChangeTimeText;
            Vector2 delta = Vector2.down * _deltaY / 9;
            AM5.transform.Translate(delta);
            AM6.transform.Translate(delta);
        }
        yield return new WaitForSeconds(_timeEnd - _timeTextChanged);
        _levelController.
        gameObject.SetActive(false);
        _levelController.LevelComplete();
    }
}
