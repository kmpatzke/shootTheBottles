using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : MonoBehaviour {

    public Transform bulletSpawn;
    public Transform ParticleParent;

    private SteamVR_TrackedObject trackedObj; // Controller and Headset
    private SteamVR_Controller.Device device; // Variable for the Controller

    //Sounds
    private AudioSource audioSource; // Used for the shot sound, the component holds the audio clip

    public GameObject muzzlePrefab; // muzzle flash animation

    private void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>(); // parent is the controller, the revolver is parented to it manually in unity
        audioSource = GetComponent<AudioSource>(); // this component is attached to the revolver
        
    }

    // Update is called once per frame
    void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index); // Get the controller
        //Raycast for Debug (visible)

       

        Debug.DrawRay(bulletSpawn.transform.position, bulletSpawn.transform.forward, Color.red); // Only in Scene view visible


        //RayCast to check what we hit
        RaycastHit hit;

        if(Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, Mathf.Infinity)) // from where to where nad get the object that is hit
        {
            if(hit.transform.tag == "Target") // The following is suppost to execute only if the player is aiming to a bottle
            {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) // checks if controller's trigger is pressed within the frame
                {
                    hit.transform.GetComponent<Bottle>().GotShot(hit.point); // calls the hit bottle's method to destroy it
                }
                
            }
        }
        DohHapticOnShoot(); // Vibriert beim Schießen
		
	}

    private void DohHapticOnShoot()
    {
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            device.TriggerHapticPulse(2000); // Vibration
           
            audioSource.PlayOneShot(audioSource.clip); // plays the shot sound

            GameObject temp = Instantiate(muzzlePrefab, ParticleParent.transform.position, ParticleParent.transform.rotation); // instantiation is enough because playOnAwake is automatically plays on awake
            
            
            Destroy(temp, 0.5f); // Doesn't let the muzzle flash play through

        }
    }
}
