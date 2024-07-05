using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMap : MonoBehaviour
{
    public Sprite Map;
    public Room Room;

    public Action<ButtonMap> Clicked;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke(this);
    }

}
