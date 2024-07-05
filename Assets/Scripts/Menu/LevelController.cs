using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level", _level);
    }

    public void StartGame()
    {
        _level = 1;
        SceneManager.LoadScene(_gameName);
    }

    public void LevelComplete()
    {
        _level = Mathf.Min(5, _level + 1);
       ContinueGame();
    }

    public void ContinueGame() => SceneManager.LoadScene(_gameName);
}
