using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private TimeController _timeController => TimeController.Instance;
    private AudioSource _audioSource;
    private BatteryController _batteryController => BatteryController.Instance;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _timeController.TimeChanged += OnTimeChanged;
        _batteryController.BatteryChanged += OnBatteryChanged;
    }

    private void OnDestroy()
    {
        _timeController.TimeChanged -= OnTimeChanged;
        _batteryController.BatteryChanged -= OnBatteryChanged;
    }

    private void OnBatteryChanged(int battery)
    {
        if (battery == 0) _audioSource.Stop();
    }

    private void OnTimeChanged(int time)
    {
        if (time == 1) _audioSource.Play();
        if (time == 355) _audioSource.Stop();
    }
}
