using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _starterText;
    [SerializeField] private GameObject _starterBackGround;
    [SerializeField] private EndView _endView;

    private TimeController _timeController => TimeController.Instance;

    private void Start()
    {
        StartCoroutine(startNight());
        _starterBackGround.SetActive(true);
        _timeController.TimeChanged += timeChanged;
    }

    private void timeChanged(int time)
    {
        if (time < 360) return;
        _endView.gameObject.SetActive(true);
        _endView.StartEnd();
    }

    private void OnDisable()
    {
        _timeController.TimeChanged -= timeChanged;
    }

    private IEnumerator startNight()
    {
        yield return new WaitForSeconds(2.5f);
        _starterText.SetActive(false);
        yield return new WaitForSeconds(3);
        _starterBackGround.SetActive(false);
        _timeController.StartTime();
    }
}
