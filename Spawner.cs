using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    bool bottleOnTheWay;

    public GameObject bottlePrefab;
	// Use this for initialization
	void Start () {
        SpawnNewBottle();
	}

    public void initializeNewBottle()
    {
        if (bottleOnTheWay) // checks if there is already a bottle in the pipeline
        { 
            return; // the return interrupts the process
        }
        bottleOnTheWay = true;
        Invoke("SpawnNewBottle", 4);
    }

	private void SpawnNewBottle()
    {
        GameObject myBottle = Instantiate(bottlePrefab, transform.position, Quaternion.Euler(-90, 0, 0));
        myBottle.transform.localScale = myBottle.transform.localScale * Random.Range(1, 4);
        bottleOnTheWay = false;
        myBottle.GetComponent<Bottle>().mySpawner = this; // if ab bottle is instantiated it need a new spawner

    }

    public void CancelSpawn()
    {
        CancelInvoke(); // used in the gameManager when the time is up
    }

}
