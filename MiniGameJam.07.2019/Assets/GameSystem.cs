using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance = null; //only one instance per game

    [SerializeField]
    private CanvasGroup gameOverBG = null;
    [SerializeField]
    private CanvasGroup gameOverImage = null;
    private bool isGameOver = false;

    // Start is called before the first frame update
    [SerializeField]
    private Image imagePrefab = null;

    [System.Serializable]
    public struct UIGroup
    {
        public Sprite sprite;
        public Stack<Image> instances;
        public GridLayoutGroup parent;

        public void Init(int amount, Image prefab)
        {
            instances = new Stack<Image>();

            for(int i = 0; i < amount; i++)
            {
                Image newImage = Instantiate(prefab);
                newImage.sprite = sprite;
                newImage.transform.SetParent(parent.transform, true);

                instances.Push(newImage);
            }
        }

        public void Decrement ()
        {
            if (instances.Count <= 0)
                return;

            Image remImage = instances.Pop();
            remImage.gameObject.SetActive(false);
        }
    }

    [SerializeField]
    private int lives = 3; //amount of lives the player has
    [SerializeField]
    private UIGroup livesUI = new UIGroup();

    [SerializeField]
    private int requiredAnswers = 5; //the required correct answers in order to proceed to the next level
    [SerializeField]
    private UIGroup answersUI = new UIGroup();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        //init the lives and required UI code:
        livesUI.Init(lives, imagePrefab);
        answersUI.Init(requiredAnswers, imagePrefab);
    }

    //called when the player answers a dialogue
    public void UpdateState (bool correct)
    {
        if(correct) //if this is a correct answer
        {
            requiredAnswers--;
            answersUI.Decrement();

            if(requiredAnswers <= 0)
            {
                //nextLevel();
            }
        }
        else //false answer
        {
            lives--;
            livesUI.Decrement();

            if (lives <= 0) //no more lives
                GameOver();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
            return;

        if (Input.anyKey)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        gameOverBG.gameObject.SetActive(true);
        gameOverImage.gameObject.SetActive(true);
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
