using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    private bool isShowing;

    public void Update()
    {
        // Pause game when pressing p
        if (Input.GetKeyDown("p"))
        {
            isShowing = !isShowing;

            if (Time.timeScale == 1 && isShowing == true)
            {
                // Time set to 0 - game paused
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
            
            pauseMenu.SetActive(isShowing);

        }

    }

    // If game pause and "p" pressed again - unpause game
    public void UnPause()
    {
        
        pauseMenu.SetActive(false);
        // Time set to 0 - game unpaused
        Time.timeScale = 1;

    }
}
