using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _indicators;
    [SerializeField] private TMP_Text _batteryText;
    [SerializeField] private GameObject _deadbackGround;

    [SerializeField] private GameObject _scream;
    private BatteryController _batteryController => BatteryController.Instance;

    private void Start()
    {
        _batteryController.BatteryChanged += OnBatteryChanged;
        _batteryController.PowerChanged += OnPowerChanged;
        OnPowerChanged((int)_batteryController.Power);
    }

    private void OnDestroy()
    {
        _batteryController.BatteryChanged -= OnBatteryChanged;
        _batteryController.PowerChanged -= OnPowerChanged;
    }

    private void OnBatteryChanged(int battery)
    {
        if (battery == 0)
        {
            kill();
        }
        _batteryText.text = $"Ёнергии осталось: {battery}%";
    }

    private void kill()
    {
        _deadbackGround.SetActive(true);
        Invoke("screamEnable", Random.Range(3, 6));
        gameObject.SetActive(false);
    }

    private void screamEnable() => _scream.SetActive(true);

    private void OnPowerChanged(int power)
    {
        _indicators.ForEach(i => i.SetActive(false));
        for (int i = 0; i < power; i++)
        {
            _indicators[i].SetActive(true);
        }
    }
}
