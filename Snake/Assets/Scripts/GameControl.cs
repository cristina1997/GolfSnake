using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code adapted from https://youtu.be/Tn3cpgdiWPI?list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA
public class GameControl : MonoBehaviour {
    public GameObject snake;
    public Snake head;
    public Snake tail;
    public string direction = "UP";
    // public int direction;
    public Vector2 newPos;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Timer", 0, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {        
        ChangeDirection();
	}

    void Timer()
    {
        Movement();
    }

    void Movement()
    {
        GameObject temp;
        newPos = head.transform.position;

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
                
        /*
            case 0:
                newPos = new Vector2(newPos.x, newPos.y + 1);
                break;
            case 1:
                newPos = new Vector2(newPos.x + 1, newPos.y);
                break;
            case 2:
                newPos = new Vector2(newPos.x, newPos.y - 1);
                break;
            case 3:
                newPos = new Vector2(newPos.x - 1, newPos.y);
                break;*/
        }

        temp = (GameObject)Instantiate(snake, newPos, transform.rotation);
        head.setNext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();

        // return;
    }

    void ChangeDirection()
    {
        // the snake does not change direction - in other words it can't go backwards
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
    }
}
