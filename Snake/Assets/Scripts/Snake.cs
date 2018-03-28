using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Code adapted from https://www.youtube.com/watch?v=Sau81jWbGRY&index=5&list=PLWeGoBm1YHVhc51TYY7fTLNbA02qkyLrA&ab_channel=InfoGamer
public class Snake : MonoBehaviour {

    private Snake next;
    public static Action<String> eat;
    
    public void setNext(Snake inside)
    {
        next = inside;
    }

    public Snake getNext()
    {
        return next;
    }

    // By removing the tail the size of the snake remains the same until it obtains food
    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider food)
    {
        // If the food is eaten, the tag of the food object is sent to the GameController script with the hit action which is Destroy()
        if(eat != null)
        {
            eat(food.tag);
        }

        // Check to see the object we hit was the food - if yes, destroy it
        if(food.tag == "Food")
        {
            Destroy(food.gameObject);
        }
    }
}
