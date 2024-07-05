using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Newspaper : MonoBehaviour
{
    private void Start() => StartCoroutine(startGame());

    private void Update()
    {
        transform.transform.localScale += Vector3.one * Time.deltaTime * 0.05f;
    }

    private IEnumerator startGame()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Game");
    }
}
