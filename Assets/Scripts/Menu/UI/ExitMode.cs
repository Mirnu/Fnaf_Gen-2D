using UnityEngine;

public class ExitMode : MenuButton
{
    protected override void OnClick() => Application.Quit();
}
