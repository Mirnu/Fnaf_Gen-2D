using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float _limit = 2.3f;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private GameObject _cameraScreen;

    private BatteryController _batteryController => BatteryController.Instance;
    private TimeController _timeController => TimeController.Instance;
    private AudioSource _audioSource;

    private float _rotation = 0;
    public bool isCamera = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _batteryController.BatteryChanged += batteryChanged;
    }

    private void OnDestroy()
    {
        _batteryController.BatteryChanged -= batteryChanged;
    }

    private void batteryChanged(int battery)
    {
        if (battery != 0) return;
        ChangeModeCamera(false);
    }

    private void Update()
    {
        if (_timeController.Time == 0) return;
        if (Input.GetKeyDown(KeyCode.Space) && _batteryController.Battery > 0) ChangeModeCamera(!isCamera);
        if (isCamera) return;
        if (Input.mousePosition.x < Screen.width * 0.2f) OnLeft();
        if (Input.mousePosition.x > Screen.width * 0.8f) OnRight();
    }

    public void ChangeModeCamera(bool active)
    {
        _audioSource.Play();
        if (active) _batteryController.AddPower();
        else _batteryController.RemovePower();
        isCamera = active;
        _cameraScreen.SetActive(isCamera);
    }

    private void OnLeft()
    {
        _rotation = Mathf.Max(_rotation - _speed * Time.deltaTime, -_limit);
        draw();
    }

    private void OnRight()
    {
        _rotation = Mathf.Min(_rotation + _speed * Time.deltaTime, _limit);
        draw();
    }

    private void draw() => Camera.main.transform.position = new Vector3(_rotation, 0, -10);
}
