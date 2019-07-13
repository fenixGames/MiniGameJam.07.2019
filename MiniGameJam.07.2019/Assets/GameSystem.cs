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

    [SerializeField]
    private AudioClip successAudio = null;
    [SerializeField]
    private AudioClip failAudio = null;

    [SerializeField]
    private float loadSceneTimer = 5.0f;
    private bool loadingScene = false;

    [System.Serializable]
    public struct UIGroup
    {
        public Sprite sprite;
        public Stack<Image> instances;
        public GridLayoutGroup parent;

        public void Init(int amount, Image prefab, bool isLife)
        {
            instances = new Stack<Image>();

            Color nextColor = Color.white;
            nextColor.a = isLife ? 1.0f : 0.3f;

            for(int i = 0; i < amount; i++)
            {
                Image newImage = Instantiate(prefab);
                newImage.sprite = sprite;
                newImage.transform.SetParent(parent.transform, true);

                newImage.color = nextColor;

                instances.Push(newImage);
            }
        }

        public void Decrement ()
        {
            if (instances.Count <= 0)
                return;

            Image nextImg = instances.Pop();
            if(nextImg.color.a == 1.0f)
            {
                Color nextColor = Color.white;
                nextColor.a = 0.2f;
                nextImg.color = nextColor;
            }
            else
                nextImg.color = Color.white;
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

    [SerializeField]
    private string nextLevelName = "next_level";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);

        //init the lives and required UI code:
        livesUI.Init(lives, imagePrefab, true);
        answersUI.Init(requiredAnswers, imagePrefab, false);
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
                AudioManager.instance.PlayAudio(successAudio);
                AudioManager.instance.UpdateMusicVolume(0.2f);
                loadingScene = true;
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
        if(loadingScene)
        {
            loadSceneTimer -= Time.deltaTime;
            if(loadSceneTimer <= 0)
                SceneManager.LoadScene(nextLevelName);
        }

        if (!isGameOver)
            return;

        if (Input.anyKey)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        gameOverBG.gameObject.SetActive(true);
        gameOverImage.gameObject.SetActive(true);
        AudioManager.instance.PlayAudio(failAudio);
        AudioManager.instance.UpdateMusicVolume(0.2f);
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
