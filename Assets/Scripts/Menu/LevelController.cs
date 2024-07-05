using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    private int _level = 1;
    public int Level => _level;

    private const string _gameName = "Newspaper";

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _level = PlayerPrefs.GetInt("level", 1);
        Debug.Log($"Current level: {_level}");
    }

    private void OnDisable()
    {
        Debug.Log($"Saved {_level}");
        PlayerPrefs.SetInt("level", _level);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        _level = 1;
        SceneManager.LoadScene(_gameName);
    }

    public void LevelComplete()
    {
        if (_level == 5) 
        {
            SceneManager.LoadScene("Menu");
            return;
        }
        _level = Mathf.Min(5, _level + 1);
       ContinueGame();
    }

    public void ContinueGame() => SceneManager.LoadScene("Game");
}
