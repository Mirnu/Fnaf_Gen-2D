using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    protected LevelController _levelController => LevelController.Instance;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick() { } 
}
