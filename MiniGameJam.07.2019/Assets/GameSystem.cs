using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance = null; //only one instance per game

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
                gameOver();
        }
    }

    void gameOver()
    {

    }
}
