using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public int numberOfAttempts = 3;
    private int numberOfSuccessfulAttempts = 0;

    private CanvasGroup gameOverBG;
    private CanvasGroup gameOverImage;
    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        numberOfSuccessfulAttempts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
    }

    void GameOver()
    {
        var bgObj = GameObject.Find("GameOverBG");
        gameOverBG = bgObj.GetComponent<CanvasGroup>();
        var imageObj = GameObject.Find("GameOverImage");
        gameOverImage = imageObj.GetComponent<CanvasGroup>();
        StartCoroutine("GameOverFadeIn");
    }

    IEnumerator GameOverFadeIn()
    {
        float time = 1f;
        while (gameOverBG.alpha < 1)
        {
            gameOverBG.alpha += Time.deltaTime / time;
            yield return null;
        }
        while (gameOverImage.alpha < 1)
        {
            gameOverImage.alpha += Time.deltaTime / time;
            yield return null;
        }
        isGameOver = true;
    }
}
