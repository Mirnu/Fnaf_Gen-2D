using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorButton : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Sprite _openDoorSprite;
    [SerializeField] private Sprite _closedDoorSprite;
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Components")]
    [SerializeField] private Animator _animator;
    private AudioSource _source;
    private BatteryController _batteryController => BatteryController.Instance;

    private bool isOpen = true;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

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
        setDoor(true);
        _source.Stop();
    }

    private void OnMouseDown()
    {
        if (_batteryController.Battery == 0) return;
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        setDoor(!isOpen);
    }

    private void setDoor(bool active)
    {
        if (!active) _batteryController.AddPower();
        else _batteryController.RemovePower();
        isOpen = active;
        _source.Play();
        _animator.SetBool("isOpen", isOpen);
        _renderer.sprite = isOpen ? _openDoorSprite : _closedDoorSprite;
    }
}
