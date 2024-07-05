using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MenuButton
{
    protected override void OnClick()
    {
       _levelController.StartGame();
    }
}
