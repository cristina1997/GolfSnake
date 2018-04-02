using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    // public SnakeHealth snakeHealth;
    public bool isGameOver;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (snakeHealth.currentHealth < 0)
        {
            isGameOver == true;               
            
        }

        if (isGameOver)
        {
            GameOver();
        }*/
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
