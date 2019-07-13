using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance = null; //only one instance per game

    private CanvasGroup gameOverBG;
    private CanvasGroup gameOverImage;
    private bool isGameOver = false;

    // Start is called before the first frame update
    [SerializeField]
    public int lives = 3; //amount of lives the player has
    [SerializeField]
    public int requiredAnswers = 5; //the required correct answers in order to proceed to the next level

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);
    }

    //called when the player answers a dialogue
    public void UpdateState (bool correct)
    {
        if(correct) //if this is a correct answer
        {
            requiredAnswers--;
            if(requiredAnswers <= 0)
            {
                //nextLevel();
            }
        }
        else //false answer
        {
            lives--;
            if (lives <= 0) //no more lives
                GameOver();
        }
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
