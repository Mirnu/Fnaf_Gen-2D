using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    public static BatteryController Instance;

    public Action<int> BatteryChanged;
    public Action<int> PowerChanged;
    private TimeController _timeController => TimeController.Instance;

    [SerializeField] private float _battery = 100;
    public float Battery => _battery;
    [SerializeField] private float _power = 1;
    public float Power
    {
        get { return _power; }
        set 
        { 
            _power = Math.Max(0, value);
            PowerChanged?.Invoke((int)_power);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeController.TimeChanged += timeChanged;
    }

    private void OnDestroy()
    {
        _timeController.TimeChanged -= timeChanged;
    }

    public void AddPower() => Power++;
    public void RemovePower() => Power--;

    private void timeChanged(int time)
    {
        _battery = math.max(0, _battery - Power * 9 / 60);
        BatteryChanged?.Invoke((int)Math.Floor(_battery));
    }
}
