using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {

   

    public Spawner mySpawner;

    public GameObject bottleBrokenPrefab;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // the gameManager 
    }

    public void GotShot(Vector3 explosion) // Explosion is the hit.point from the revolver script
    {
        gameManager.IncreaseScore();
        if (gameManager.gameStarted != true) // The game starts with the first shot bottle
        {
            gameManager.gameStarted = true;
        }
        
        
        mySpawner.initializeNewBottle(); // method is called as soon as one bottle gets hit
        GameObject destObj = Instantiate(bottleBrokenPrefab, transform.position, transform.rotation); //replaces the hit bottle with a fractured model
        destObj.transform.localScale = gameObject.transform.localScale;
        destObj.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x , gameObject.transform.rotation.y, gameObject.transform.rotation.z);

        foreach(MeshCollider collider in destObj.GetComponentsInChildren<MeshCollider>())
        {
            collider.convex = true; // has to be switched on in order to be able to collide with other mesh colliders. 
            Rigidbody rig = collider.gameObject.AddComponent<Rigidbody>(); // is needed to add explosion force in the next step
            rig.AddExplosionForce(1000, explosion, 5f);
        }


        Destroy(destObj, 4); // destroys fractures after 4 seconds
        Destroy(gameObject); // destroys the bottle itself


    }

	
}
