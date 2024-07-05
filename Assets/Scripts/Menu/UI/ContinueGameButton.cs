using UnityEngine;

public class ContinueGameButton : MenuButton
{
    private void Start()
    {
        gameObject.SetActive(_levelController.Level > 1);
    }

    protected override void OnClick()
    {
        Debug.Log("Continue");
        _levelController.ContinueGame();
    }
}
