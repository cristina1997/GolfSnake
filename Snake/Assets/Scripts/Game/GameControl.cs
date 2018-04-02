using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Code adapted from https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
public class GameControl : MonoBehaviour {
    public int maxSize, currentSize;
    public int xSpawn, ySpawn;
    public int score;
    public GameObject snake, food, currentFood;
    public Snake head, tail;
    public string direction;
    public Vector2 newPos;
    private Quaternion rotation;
    public Text scoreText;

    private void OnEnable()
    {
        Snake.eat += Eat;    
    }

    private void OnDisable()
    {
        Snake.eat -= Eat;
    }

    // Use this for initialization
    void Start () {
        // The snake moves every 0.5 seconds
        InvokeRepeating("Timer", 0, 0.5f);
        CreateFood();
        direction = "UP";
        currentSize = 1;
        maxSize = 2;
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        PreventBackwardMovement();
	}

    void Timer()
    {
        Movement();
        StartCoroutine(CheckVisible());

        // Keeps the snake from growing any longer if it's at its maximum size
        if (currentSize >= maxSize)
        {
            UpdateTail();
        }
        // The snake grows to be at its maxSize if the maxSize is bigger
        else
        {
            currentSize++;
        }

    } // Timer

    void Movement()
    {
        GameObject temp;
        newPos = head.transform.position;   // set to the current position of our head object
        RotateSnake();

        switch (direction)
        {
            case "UP":
                newPos = new Vector2(newPos.x, newPos.y + 1);
                break;
            case "RIGHT":
                newPos = new Vector2(newPos.x + 1, newPos.y);
                break;
            case "DOWN":
                newPos = new Vector2(newPos.x, newPos.y - 1);
                break;
            case "LEFT":
                newPos = new Vector2(newPos.x - 1, newPos.y);
                break;

        }


        temp = (GameObject)Instantiate(snake, newPos, rotation/*transform.rotation*/);                                  //
        head.setNext(temp.GetComponent<Snake>());                                                           // set the next variable of our current head to the new instantiated object
        head = temp.GetComponent<Snake>();                                                                  // reset the current head to the temp object 

        // return;
    } // Movement

    private void RotateSnake()
    {
        if (direction == "UP" || direction == "DOWN")
        {
            rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        else if (direction == "LEFT" || direction == "RIGHT")
        {
            rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    } // RotateSnake

    void PreventBackwardMovement()
    {
        // It prevents the snake from going backwards
        if (direction != "DOWN" && Input.GetKey("up"))
        {
            direction = "UP";
        }
        if (direction != "UP" && Input.GetKey("down"))
        {
            direction = "DOWN";
        }
        if (direction != "LEFT" && Input.GetKey("right"))
        {
            direction = "RIGHT";
        }
        if (direction != "RIGHT" && Input.GetKey("left"))
        {
            direction = "LEFT";
        }
    } // PreventBackwardMovement

    void UpdateTail()
    {
        Snake tempTail= tail;
        tail = tail.getNext();
        tempTail.RemoveTail();
    } // UpdateTail

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
            maxSize++;
            score += 10;
            scoreText.text = score.ToString();
            int temp = PlayerPrefs.GetInt("HighScore");

            if (score > temp)
            {
                PlayerPrefs.SetInt("HighScore", score);
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

    void Wrap()
    {
        switch (direction)
        {
            // If snake goes off the top of the screen it appears at the bottom and vice versa
            // Otherwise if snake goes off left side of the screen it appears on the right and vice versa
            case "UP":

                // y position going of the right side is positive
                    // y position decremented by 1 - negative and then flips it
                head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y - 1));
                break;
            case "RIGHT":

                // x position going of the right side is positive
                    // x position decremented by 1 - negative and then flips it
                head.transform.position = new Vector2(-(head.transform.position.x - 1), head.transform.position.y);
                break;
            case "DOWN":

                // y position going of the right side is negative
                    // y position decremented by 1 - positive and then flips it
                head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y + 1));
                break;
            case "LEFT":

                // x position going of the right side is negative
                    // x position decremented by 1 - positive and then flips it
                head.transform.position = new Vector2(-(head.transform.position.x + 1), head.transform.position.y);
                break;
        }
    } // Wrap

    IEnumerator CheckVisible()
    {
        // Wait 'til end of frame to execute code after it
        yield return new WaitForEndOfFrame();      
        
        // Check if current object is visible
        if (!head.GetComponent<Renderer>().isVisible)
        {
            Wrap();
        }

    } // CheckRenderer
    
}
