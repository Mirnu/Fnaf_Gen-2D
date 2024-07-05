using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreamerView : MonoBehaviour
{
    [SerializeField] private Sprite _starterSprite;
    [SerializeField] private Sprite _enderSprite;

    [SerializeField] private CameraMovement cameraMovement;

    [SerializeField] private GameObject _deadView;

    public Action Scream;

    private SpriteRenderer _spriteRenderer;
    private int _ticks = 20;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _starterSprite;
    }

    private void Start() => StartCoroutine(Attack());

    private IEnumerator Attack()
    {
        Scream?.Invoke();
        if (cameraMovement.isCamera) cameraMovement.ChangeModeCamera(false);
        WaitForSeconds delta = new WaitForSeconds(1 / _ticks);
        for (int i = 0; i < _ticks; i++)
        {
            transform.localScale += Vector3.one * 0.5f / _ticks;
            yield return delta;
        }
        _spriteRenderer.sprite = _enderSprite;
        yield return new WaitForSeconds(1);
        _deadView.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
