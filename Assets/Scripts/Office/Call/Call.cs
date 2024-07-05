using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Call : MonoBehaviour
{
    [SerializeField] private Button _button;
    private TimeController _timeController => TimeController.Instance;
    private BatteryController _batteryController => BatteryController.Instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button.onClick.AddListener(stopCall);
    }

    private void stopCall()
    {
        _batteryController.BatteryChanged -= OnBatteryChanged;
        _audioSource.Stop();
        Destroy(_button.gameObject);
        Destroy(gameObject);
    }

    private void Start()
    {
        _timeController.TimeChanged += OnStartGame;
        _batteryController.BatteryChanged += OnBatteryChanged;
    }
    private void OnStartGame(int time)
    {
        if (time == 1)
        {
            StartCoroutine(startCheck());
            _audioSource.Play();
        }
        else if (time == 359 && _audioSource != null)
        {
            _audioSource?.Stop();
        }
    }

    private void OnBatteryChanged(int battery)
    {
        if (battery == 0) stopCall();
    }

    private IEnumerator startCheck()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        if (gameObject == null) yield break;
        stopCall();
    }
}
