using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeHealth : MonoBehaviour {
    public const int healthy = 2, unhealthy = -10, hit = -2;
    public int maxHealth = 100;                            
    public int currentHealth;                                                  
    bool isDamaged;

    // Use this for initialization
    void Awake () {
        // Set the initial health of the player.
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (SnakeFood.isDead || currentHealth == 0)
        {
            SnakeFood.isDead = false;
            GameOver();
        }   
	}

    public void HealthLoss(int amount)
    {
        if (!(currentHealth + amount > maxHealth))
        {
            currentHealth += amount;
        }

    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
        
    } // GameOver
}
