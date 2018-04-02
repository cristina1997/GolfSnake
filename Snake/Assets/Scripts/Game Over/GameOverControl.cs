using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverControl : MonoBehaviour {

    public Text highScore;

    // Use this for initialization
    void Start()
    {
        HighScore();

    }

    public void PlayAgain()
    {

        SceneManager.LoadScene(1);
    }

    void HighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Quit()
    {

        Application.Quit();
    }
}
