using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHealth : MonoBehaviour {
    public int startingHealth = 100;                            
    public int currentHealth;                                  
    bool isDead;                                                
    bool damaged;
    bool isHungry;

    // Use this for initialization
    void Start () {
        // Set the initial health of the player.
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
		if (SnakeFood.isHealthy == false)
        {
            isHungry = true;
        }
        else
        {
            isHungry = false;
        }

        HealthLoss();        
	}

    private void HealthLoss()
    {
        if (isHungry)
        {

        }
    }
}
