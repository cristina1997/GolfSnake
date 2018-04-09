using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code adapted from:
//      - https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
//      - https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/adding-mobile-controls

public class SnakeMovement : MonoBehaviour
{
    public int newMaxSize, maxSize, currentSize;
    public GameObject snake;
    public Snake head, tail;
    public SnakeFood snakeFood;
    public Vector2 newPos;
    public Text speedDisplay;
    public Slider speedSlider;
    public int outputDisplaySpeed;
    public string direction;
    public const float speedTimerInit = 0.05f, speedResetInit = 300f, startSpeedValue = 0.5f;
    public float speedTimerCountdown, speedResetCountdown;
   // private Vector2 touchOrigin = -Vector2.one;                                 
    // public int horizontal, vertical;
    private void Start()
    {
        speedDisplay = GameObject.Find("SpeedText").GetComponent<Text>();
        speedSlider = GameObject.Find("SpeedUI").GetComponent<Slider>();
        InvokeRepeating("Timer", 0, startSpeedValue);
        speedTimerCountdown = startSpeedValue;
        direction = "UP";
        currentSize = 1;
        maxSize = 2;
    }

    // It calculates the amount by which the speed is increased
    public void SpeedCalculation()
    {

        // InvokeRepeating() - The speed is increased by decreasing the value of the last parameter in this method
        // CancelInvoke() - It makes sure the snake's speed doesn't affect the smoothness of the snake's movement
        outputDisplaySpeed += 20;
        speedTimerCountdown -= 0.05f;
        CancelInvoke("Timer");
        InvokeRepeating("Timer", 0, speedTimerCountdown);

    } // SpeedCalculation

    // It resets the snake's speed to its starting value every 5 minutes 
    public void SpeedReset()
    {
        // Countdown from 5 minutes
        float speedResetCountdown = speedResetInit;
        speedResetCountdown -= Time.time;

        // After the 5 minutes run both, the snake's speed and the 5 minute countdown that ran out are reset to their original values
        if (speedResetCountdown < 0)
        {
            speedResetCountdown = speedResetInit;
            speedTimerCountdown = startSpeedValue;
            outputDisplaySpeed = 0;
        } // if
    } // SpeedReset

    private void Update()
    {

        ChangeDirectionPC();
       // ChangeDirectionMob();
        SpeedReset();        
        speedDisplay.text = "Speed: " + outputDisplaySpeed;
        speedSlider.value = outputDisplaySpeed;

        // Calculates the new maximum size of the snake
        newMaxSize = SnakeFood.foodEaten;
        newMaxSize += maxSize;

        // The speed is only calculated if the snake is set to be faster - isFaster = true
        // And if the speed does not go below 0.03f - if it's too low the snake will go too fast
        if (SnakeFood.isFaster && speedTimerCountdown >= 0.25f)
        {
            SpeedCalculation();
        } // if
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
        } // if...else if

    } // Timer

    void Movement()
    {
        GameObject temp;
        newPos = head.transform.position;   // set to the current position of our head object

        // The snake moves in the direction set by adding a 1 to the position it is moving towards
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
        } // switch

        temp = (GameObject)Instantiate(snake, newPos, transform.rotation);                                  // it sets the new position for the snake - movement
        head.setNext(temp.GetComponent<Snake>());                                                           // set the next variable of our current head to the new instantiated object
        head = temp.GetComponent<Snake>();                                                                  // reset the current head to the temp object 

    } // Movement

    void UpdateTail()
    {
        Snake tempTail = tail;
        tail = tail.getNext();
        tempTail.RemoveTail();
    } // UpdateTail


    public void ChangeDirectionPC()
    {
        // It prevents the snake from going backwards on the pc and changes the direction of the snake in accordance to the arrow keys on the keyboard
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
        } // if...else if

    } // ChangeDirectionPC

    public void ChangeDirectionMobile(string dir)
    {

        // It prevents the snake from going backwards on the pc and changes the direction of the snake in accordance to the arrow buttons on the touchpad
        if (direction != "DOWN" && dir == "UP")
        {
            direction = dir;
        }
        if (direction != "UP" && dir == "DOWN")
        {
            direction = dir;
        }
        if (direction != "LEFT" && dir == "RIGHT")
        {
            direction = dir;
        }
        if (direction != "RIGHT" && dir == "LEFT")
        {
            direction = dir;
        } // if...else if

    } // ChangeDirectionMobile

    /*public void ChangeDirectionMob()
    {
        horizontal = 0;
        vertical = 0;

        // Check if we are running on Windows Phone 10
        #if UNITY_WEBPLAYERUNITY_WIN10

            // Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {

                // Store the first touch detected
                Touch myTouch = Input.touches[0];
                
                // Check if the phase of that touch Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    // If so, set touchOrigin to the position of that touch
                    touchOrigin = myTouch.position;
                } // If the touch phase did not begin but Ended instead and the x of touchOrigin is greater or equal to zero:
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    
                    // If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                    Vector2 = touchEnd = myTouch.position;

                    // Calculate the difference between the beginning and end of the touch on the x axis.
                    float x = touchEnd.x - touchOrigin.x;

                    // Calculate the difference between the beginning and end of the touch on the y axis.            
                    float y = touchEnd.y - touchOrigin.y;

                    // Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                    touchOrigin.x = -1;

                    // Check if the difference along the x axis is greater than the difference along the y axis.        
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        // If x is greater than zero, set horizontal to 1
                        // Otherwise set it to -1    
                        horizontal = x > 0 ? 1 : -1;
                    else
                        // If y is greater than zero, set horizontal to 1
                        // Otherwise set it to -1
                        vertical = y > 0 ? 1 : -1;
                } // if...else if
            } // if
    #endif
        if (horizontal != 0 || vertical != 0)
        {
            if (vertical > 0)
            {
                direction = "UP";
            }
            else if (vertical < 0)
            {
                direction = "DOWN";
            }
            else if (horizontal > 0)
            {
                direction = "RIGHT";
            }
            else if (horizontal < 0)
            {
                direction = "LEFT";
            } // if...else if
        } // if

    } // ChangeDirectionMob
    */

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
        } // switch
    } // Wrap

    IEnumerator CheckVisible()
    {
        // Wait 'til end of frame to execute code after it
        yield return new WaitForEndOfFrame();

        // Check if current object is visible
        if (!head.GetComponent<Renderer>().isVisible)
        {
            Wrap();
        } // if

    } // CheckRenderer

} // SnakeMovement
