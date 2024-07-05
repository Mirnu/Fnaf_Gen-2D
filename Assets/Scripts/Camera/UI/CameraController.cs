using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Animatronic")]
    [SerializeField] private Animatronic _animatronic;

    [Header("UI")]
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private Image _mapImage;
    [SerializeField] private Image _animatronicImage;
    [SerializeField] private List<ButtonMap> _buttons;

    private AudioSource _audioSource;

    private int _currentCam = 1;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked += camChanged;
        }
        _animatronic.PointChanged += animatronicPointChanged;
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.Clicked -= camChanged;
        }
        _animatronic.PointChanged -= animatronicPointChanged;
    }

    private void animatronicPointChanged(WalkPoint point) => checkAnimatronic();


    private void camChanged(ButtonMap button)
    {
        _currentCam = button.Room.Camera;
        _audioSource.Play();
        _backGroundImage.sprite = button.Room.BackGrounds[0];
        _mapImage.sprite = button.Map;
        checkAnimatronic();
    }

    private void checkAnimatronic()
    {
        _animatronicImage.gameObject.SetActive(_animatronic.CurrentPoint.Point == _currentCam);
        _animatronicImage.sprite = _animatronic.GetRandomSprite();
    }
}
