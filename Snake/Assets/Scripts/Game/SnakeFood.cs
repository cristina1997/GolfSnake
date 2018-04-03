using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code adapted from https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
public class SnakeFood : MonoBehaviour {
    public GameObject food, currentFood;
    public SnakeHealth snakeHealth;
    public const float speedTimeInit = 90f, healthyFoodTimeInit = 300f;
    public const int healthy = 2, unhealthy = -5;
 
    public int xSpawn, ySpawn;
    public static int foodEaten;
    public Text scoreText;
    public int score;

    public static int speedUpFood, healthyFood;
    public static bool isFaster, isHealthy;    
    public static bool isDead;

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
        snakeHealth = GetComponent<SnakeHealth>();
        CreateFood();
        foodEaten = 0;
        speedUpFood = 0;
        healthyFood = 0;
    }

    private void Update()
    {
        Speed();
        Health();        
    }

    private void Speed()
    {
        // Countdown from 1.5 minutes
        float foodSpeedCountDown = speedTimeInit;
        foodSpeedCountDown -= Time.time;

        if (foodSpeedCountDown < 0 || (foodSpeedCountDown >= 0 && speedUpFood <= 3))
        {

            // If the snake picks up more than 3 food objects in the space of 1.5 minutes the snake is said to be faster - isFaster = true
            // Otherwise - isFaster = false
            if (speedUpFood >= 3)
            {
                isFaster = true;                
                speedUpFood = 0;

            }
            else if (speedUpFood < 3)
            {
                isFaster = false;
            }

            // The countdown of the timer during which the snake can eat enough food to increase its speed is reset to its original value
            foodSpeedCountDown = speedTimeInit;
        }

        /*
        Debug.Log("isFaster: " + isFaster);
        Debug.Log("speedUpFood: " + speedUpFood);
        Debug.Log("foodSpeedCountDown: " + foodSpeedCountDown);*/

    }

    float healthCountDown = healthyFoodTimeInit;
    private void Health()
    {
        // Countdown from 5 minutes
        healthCountDown -= Time.deltaTime;

        if (healthCountDown < 0)
        {
            VerifyHealth();            

            // The countdown of the timer during which the snake can eat enough food to keep himself alive is reset to its original value
            healthCountDown = healthyFoodTimeInit;
        }

        /*
        Debug.Log("isHealthy: " + isHealthy);
        Debug.Log("speedUpFood: " + speedUpFood);
        Debug.Log("healthCountDown: " + healthCountDown);*/

    } // Health

    private void VerifyHealth()
    {
        // If the snake picks up more than 10 food objects in the space of 5 minutes the snake is said to be healthy - isHealthy = true
        // Otherwise - isHealthy = false
        if (healthyFood >= 10)
        {
            isHealthy = true;
            healthyFood = 0;
        }
        else if (healthyFood < 10)
        {
            isHealthy = false;
        }

        // If the snake is healthy then the HP increases by 2
        // Otherwise it decreases by 5
        if (isHealthy)
        {
            snakeHealth.HealthLoss(healthy);
        }
        else
        {
            snakeHealth.HealthLoss(unhealthy);
        }
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

    // If the game object is spawned within camera view it's set to fals. It is set back to true at the end of the first frame instantiated
    // If the game object is spawned off view of our camera, it will always be false.
    IEnumerator CheckRenderer(GameObject inside)
    {
        // It waits until the end of the current frame and then executes code after it.    
        yield return new WaitForEndOfFrame();                                                                   

        // Check to see if food is visible
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

            
            foodEaten++;                                                                                    // Counts the amount of food eaten by the snake
            speedUpFood++;                                                                                  // Counts the amount of food eaten by the snake that is needed for the snake to speed up
            healthyFood++;                                                                                  // Counts the amount of food eaten by the snake that is needed for the snake to not lose health

            score += 10;
            scoreText.text = score.ToString();                                                              // Outputs the score to the text field in the Score game object

            int temp = PlayerPrefs.GetInt("HighScore", 0);

            if (score > temp)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }

        if (eatenObject == "Snake")
        {
            // Prevents snake from moving any further
            CancelInvoke("Timer");
            isDead = true;
        }

    } // Eat

}
