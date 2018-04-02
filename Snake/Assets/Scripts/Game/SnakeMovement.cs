using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    public int newMax, maxSize, currentSize;
    public GameObject snake;
    public Snake head, tail;
    public SnakeFood snakeFood;
    public string direction;
    public Vector2 newPos;
    private Quaternion rotation;

    private void Start()
    {
        InvokeRepeating("Timer", 0, 0.5f);
        direction = "UP";
        currentSize = 1;
        maxSize = 2;
    }

    private void Update()
    {
        PreventBackwardMovement();

        // Calculates the new maximum size of the snake
        newMax = SnakeFood.foodEaten;
        newMax += maxSize;
    }

    void Timer()
    {
        Movement();
        StartCoroutine(CheckVisible());
                
        // Keeps the snake from growing any longer if it's at its maximum size 

        if (currentSize >= newMax)
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



    /*public int maxSize, currentSize;
    public GameObject snake;
   // public SnakeFood snakeFood;
    public Snake head, tail;
    public string direction;
    public Vector2 newPos;
    private Quaternion rotation;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Timer", 0, 0.5f);
        direction = "UP";
        currentSize = 1;
        maxSize = 2;

        // GameObject snake = GameObject.Find("Snake");
       // snakeFood = snake.GetComponent<SnakeFood>();
    }
	
	// Update is called; once per frame
	void Update () {
        PreventBackwardMovement();
        // maxSize += snakeFood.addedFood;
    }


    void Timer()
    {
        Movement();
        StartCoroutine(CheckVisible());
        // maxSize += addedFood;
        // Debug.Log(maxSize);

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


        temp = (GameObject)Instantiate(snake, newPos, rotation); //transform.rotation);                                  //
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
*/

}
