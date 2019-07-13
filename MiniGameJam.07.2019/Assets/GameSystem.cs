using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int numberOfAttempts = 3;
    private int numberOfSuccessfulAttempts = 0;

    // Start is called before the first frame update
    void Start()
    {
        numberOfSuccessfulAttempts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.numberOfAttempts <= 0)
            this.gameOver();
    }

    void gameOver()
    {

    }
}
