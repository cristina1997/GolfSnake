using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour {
    public Text highScore;

    // Use this for initialization
    void Start()
    {
        HighScore();

    }

    public void Menu()
    {

        SceneManager.LoadScene(0);
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
