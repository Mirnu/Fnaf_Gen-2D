using System;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    [SerializeField] private GameObject _backGround;
    [SerializeField] private GameObject _button;

    public Action<bool> ActiveChanged;

    private BatteryController _batteryController => BatteryController.Instance;

    public bool isActive = false;

    private void Start()
    {
        _batteryController.BatteryChanged += batteryChanged;
    }

    private void OnApplicationQuit()
    {
        _batteryController.BatteryChanged -= batteryChanged;
    }

    private void batteryChanged(int battery)
    {
        if (battery != 0) return;
        setLight(false);
    }

    private void OnMouseDown()
    {
        if (_batteryController.Battery == 0) return;
        setLight(!isActive);
    }

    private void setLight(bool active)
    {
        if (active) _batteryController.AddPower();
        else _batteryController.RemovePower();
        isActive = active;
        _backGround.SetActive(isActive);
        _button.SetActive(isActive);
        ActiveChanged?.Invoke(isActive);
    }
}
