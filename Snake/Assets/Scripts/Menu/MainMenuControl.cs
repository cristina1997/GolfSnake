using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {
    public Text highScore;

	// Use this for initialization
	void Start () {
        HighScore();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    void HighScore()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

}
