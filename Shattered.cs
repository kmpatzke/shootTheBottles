using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shattered : MonoBehaviour {

    public AudioClip[] clips = new AudioClip[5]; // five different shattering sounds
    private AudioSource aSource;

    

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
        aSource.clip = clips[Random.Range(0, 5)];
        aSource.PlayOneShot(aSource.clip);
    }
}
