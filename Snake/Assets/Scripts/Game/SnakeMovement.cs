using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code adapted from https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
public class SnakeMovement : MonoBehaviour {
    public int newMaxSize, maxSize, currentSize;
    public GameObject snake;
    public Snake head, tail;
    public SnakeFood snakeFood;
    public string direction;
    public Vector2 newPos;
    private Quaternion rotation;
    public const float speedTimerInit = 0.05f, speedResetInit = 300f, startSpeedValue = 0.5f;
    public float speedTimerCountdown, speedResetCountdown;
    
    private void Start()
    {
        speedTimerCountdown = startSpeedValue;

        InvokeRepeating("Timer", 0, speedTimerCountdown);
        direction = "UP";
        currentSize = 1;
        maxSize = 2;
    }
    
    public void SpeedCalculation()
    {
       if (SnakeFood.healthyFood != 0)
        {
            speedTimerCountdown -= 0.05f;
            CancelInvoke("Timer");
            InvokeRepeating("Timer", 0, speedTimerCountdown);
        }
    }

    public void SpeedReset()
    {        

        float speedResetCountdown = speedResetInit;
        speedResetCountdown -= Time.time;
                
        if (speedResetCountdown < 0)
        {
            speedResetCountdown = speedResetInit;
            speedTimerCountdown = startSpeedValue;
        }
    }

    private void Update()
    {
        PreventBackwardMovement();
        SpeedReset();

        // Calculates the new maximum size of the snake
        newMaxSize = SnakeFood.foodEaten;
        newMaxSize += maxSize;

        if (SnakeFood.isFaster && speedTimerCountdown >= 0.3f)
        {
            SpeedCalculation();
        }
    }

    void Timer()
    {
        Movement();
        StartCoroutine(CheckVisible());
                
        // Keeps the snake from growing any longer if it's at its maximum size 

        if (currentSize >= newMaxSize)
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

    } // Movement

    void UpdateTail()
    {
        Snake tempTail = tail;
        tail = tail.getNext();
        tempTail.RemoveTail();
    } // UpdateTail

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
