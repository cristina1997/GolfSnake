using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code adapted from https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
public class SnakeFood : MonoBehaviour {
    public int xSpawn, ySpawn;
    public static int foodEaten;
    public int score;
    public GameObject food, currentFood;
    public Text scoreText;

    private void OnEnable()
    {
        Snake.eat += Eat;    
    }

    private void OnDisable()
    {
        Snake.eat -= Eat;
    }

    private void Start()
    {
        CreateFood();
        foodEaten = 0;
    }

    void CreateFood()
    {
        // Vector 2 position to instantiate the food at
        int xTemp = Random.Range(-xSpawn, xSpawn);
        int yTemp = Random.Range(-ySpawn, ySpawn);

        // Instantiate food object
        currentFood = (GameObject)Instantiate(food, new Vector2(xTemp, yTemp), Quaternion.Euler(new Vector3(90, 0, 0)));
        StartCoroutine(CheckRenderer(currentFood));

    } // CreateFood

    IEnumerator CheckRenderer(GameObject inside)
    {
        // Check if food is visible
        // If food spawned within camera view it's set to false and is set to true at the end of the first frame instantiated
        // If spawned off view of our camera it will always be false
        yield return new WaitForEndOfFrame();                                                           // Waits until the end of the current frame and then execute code after it

        // Check to see if game object passed into this function is visible
        if (inside.GetComponent<Renderer>().isVisible == false)
        {
            // Make sure we have food object
            if (inside.tag == "Food")
            {
                Destroy(inside);
                CreateFood();
            }
        }
    } // CheckRenderer

    void Eat(string eatenObject)
    {
        // Make sure eatenObejct received from Snake is the food
        if (eatenObject == "Food")
        {
            // Instantiate new food Object
            CreateFood();
            foodEaten++;
            score += 10;
            scoreText.text = score.ToString();
            int temp = PlayerPrefs.GetInt("HighScore");

            if (score > temp)
            {
                PlayerPrefs.SetInt("HighScore", 0);
            }
        }

        if (eatenObject == "Snake")
        {
            // Prevents snake from moving any further
            CancelInvoke("Timer");
            GameOver();
        }

    } // Eat

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
