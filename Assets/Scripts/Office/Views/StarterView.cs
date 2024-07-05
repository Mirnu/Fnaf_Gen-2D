using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nightText;
    private LevelController _levelController => LevelController.Instance;

    private void Start()
    {
        _nightText.text = _levelController.Level == 3 
            ? "3яя Ночь" 
            : $"{_levelController.Level}ая Ночь";
    }
}
