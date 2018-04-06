using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeHealth : MonoBehaviour {
    public const int healthy = 2, unhealthy = -10, hit = -2;
    public Text healthDisplay;
    public Slider healthSlider;
    public int maxHealth = 100;                            
    public static int currentHealth;                                                  
    bool isDamaged;

    // Use this for initialization
    void Awake () {
        // Set the initial health of the player.
        
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        healthDisplay = GameObject.Find("HealthText").GetComponent<Text>();
        healthSlider = GameObject.Find("HealthUI").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update ()
    {
        int newCurrentHealth = SnakeHealth.currentHealth;
        healthDisplay.text = "Health: " + newCurrentHealth;
        healthSlider.value = newCurrentHealth;      

        if (SnakeFood.isDead || newCurrentHealth == 0)
        {
            SnakeHealth.currentHealth = maxHealth;
            SnakeFood.isDead = false;
            GameOver();
        } // if
	}

    public void HealthLoss(int amount)
    {
        
        if (!(currentHealth + amount > maxHealth))  
        {            
            currentHealth += amount;
        } // if

    } // HealthLoss

    public void GameOver()
    {
        SceneManager.LoadScene(2);
        
    } // GameOver
} // SnakeHealth
