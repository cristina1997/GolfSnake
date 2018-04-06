using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoles : MonoBehaviour
{
    public SnakeHealth snakeHealth;
    public const int hit = -2;
    public static bool snakeInRange;

    private void Start()
    {
        snakeHealth = GetComponent<SnakeHealth>();
    }

    void OnTriggerEnter2D(Collider2D snake)
    {
        
        // If the entering collider is the player...
        if (snake.gameObject.CompareTag("Snake"))
        {
            // ... the player is in range.
            snakeInRange = true;            
        } // if
        VerifyHit();
    } // OnTriggerEnter2D

    // If the snake and the black hole collide it sends the amount of damage taken to HealthLoss
    public void VerifyHit()
    {        
        if (snakeInRange)
        {
            snakeHealth.HealthLoss(hit);
        } // if
    } // VerifyHit
} // BlackHoles
