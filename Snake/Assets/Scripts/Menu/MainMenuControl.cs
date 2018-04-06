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
        // highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    public void Play()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(1);
    } // Play

    void HighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();

    } // HighScore

    public void Quit()
    {

        Application.Quit();
    } // Quit

} // MainMenuControl
