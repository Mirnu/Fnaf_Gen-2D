using UnityEngine;

public class SideAnimatronicView : MonoBehaviour
{
    [SerializeField] private GameObject _leftAnimatronic;
    [SerializeField] private GameObject _rightAnimatronic;
    [SerializeField] private LightButton _leftLight;
    [SerializeField] private LightButton _rightLight;
    [SerializeField] private Animatronic _animatronic;
    [SerializeField] private ScreamerView _screamerView;

    private void Awake()
    {
        _screamerView.Scream += OnScream;
        _animatronic.PointChanged += OnPointChanged;
        _leftLight.ActiveChanged += OnLightChanged;
        _rightLight.ActiveChanged += OnLightChanged;
    }

    private void OnDestroy()
    {
        _screamerView.Scream -= OnScream;
        _animatronic.PointChanged -= OnPointChanged;
        _leftLight.ActiveChanged -= OnLightChanged;
        _rightLight.ActiveChanged -= OnLightChanged;
    }

    private void OnLightChanged(bool active) => changeLight();

    private void OnScream()
    {
        _leftAnimatronic.SetActive(false);
        _rightAnimatronic.SetActive(false);
    }

    private void OnPointChanged(WalkPoint point) => changeLight();

    private void changeLight()
    {
        _leftAnimatronic.SetActive(_animatronic.CurrentPoint.Point == 5 && _leftLight.isActive);
        _rightAnimatronic.SetActive(_animatronic.CurrentPoint.Point == 8 && _rightLight.isActive);
    }
}
