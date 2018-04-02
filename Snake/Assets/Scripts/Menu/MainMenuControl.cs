using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {
    public Text highScore;

	// Use this for initialization
	void Start ()
    {
        HighScore();

    }

    public void Play()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

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
