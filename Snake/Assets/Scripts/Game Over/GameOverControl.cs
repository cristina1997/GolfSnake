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
        // highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        HighScore();

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    } // PlayAgain

    void HighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    } // HighScore

    public void Quit()
    {
        Application.Quit();
    } // Quit

    public void DeleteScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    } // DeleteScore

} // GameOverControl
