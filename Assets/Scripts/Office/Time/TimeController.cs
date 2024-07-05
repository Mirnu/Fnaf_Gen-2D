using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;

    public static TimeController Instance;
    public Action<int> TimeChanged;

    private int _time = 0;
    private WaitForSeconds _delta;
    public int Time => _time;

    private void Awake()
    {
        Instance = this;
        _delta = new WaitForSeconds(_speed);
    }

    public void StartTime()
    {
        StartCoroutine(tick());
    }

    private IEnumerator tick()
    {
        for (int i = 0; i < 360; i++)
        {
            yield return _delta;
            _time++;
            TimeChanged?.Invoke(_time);
        }
    }
}
