using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    private TMP_Text _timeText;
    [SerializeField] private TMP_Text _nightText;
    private TimeController _timeController => TimeController.Instance;
    private LevelController _levelController => LevelController.Instance; 
    private BatteryController _batteryController => BatteryController.Instance;

    private void Start()
    {
        _timeText = GetComponent<TMP_Text>();
        _timeText.text = "12 PM";
        _nightText.text = "Ночь " + _levelController.Level;
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
        if (battery != 0) return;
        _nightText.gameObject.SetActive(false);
        _timeText.gameObject.SetActive(false);
    }

    private void OnTimeChanged(int time)
    {
        int hours = time / 60 < 1 ? 12 : time / 60;
        _timeText.text = hours.ToString() + (hours == 12 ? " PM" : "AM");
    }
}
