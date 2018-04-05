using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {
    public const int maxBlackHoles = 8;
    public GameObject blackHole, newBlackHole;           
    public Vector3 center, size;
    public int xSpawn, ySpawn;

    // Use this for initialization
    void Start () {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        // InvokeRepeating("Spawn", 0, 0);
        CreateObstacle();

    }
	
	// Update is called once per frame
	void Update () {
      
    }

    void CreateObstacle()
    {

        for (int i = 0; i < maxBlackHoles; i++)
        {
            int xTemp = Random.Range(-xSpawn, xSpawn);
            int yTemp = Random.Range(-ySpawn, ySpawn);

            // Instantiate black hole object
            newBlackHole = (GameObject)Instantiate(blackHole, new Vector2(xTemp, yTemp), transform.rotation);
            // StartCoroutine(CheckRenderer(newBlackHole));
          
        } // for
        
    } // CreateFood

    // If the game object is spawned within camera view it's set to falsee. It is set back to true at the end of the first frame instantiated
    // If the game object is spawned off view of our camera, it will always be false.
    private IEnumerator CheckRenderer(GameObject inside)
    {
       
        // It waits until the end of the current frame and then executes code after it.    
        yield return new WaitForEndOfFrame();

        // Check to see if black object is visible
        if (inside.GetComponent<Renderer>().isVisible == false)
        {
            Debug.Log("Hi");
    
            // Make sure we have food object
            // If the black hole collides with the food, the snake or another black hole then it is destroyed and another one is created in it splace

        } //  if

    }

    /* void OnDrawGizmosSelected()
     {
          Gizmos.color = new Color(1, 0, 0, 0.5f);
          Gizmos.DrawCube(center);
      }*/
}
                                                                                                                                                                                                                              