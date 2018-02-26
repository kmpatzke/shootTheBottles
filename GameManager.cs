using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int score;
    public float timer = 30;
    public Text scoreText;
    public Text timerText;

    public bool gameStarted = false;

    private void Update()
    {
        if (gameStarted == true)
        {
            timer -= 1 * Time.deltaTime; // Timer counts down
            timerText.text = "Time: " + timer.ToString("F0"); 

            if(timer <= 0)
            {
                Bottle[] bottles = FindObjectsOfType<Bottle>(); // get all Bottles and destroy them
                foreach(Bottle bottle in bottles)
                {
                    Destroy(bottle);
                }
                Spawner[] spawners = FindObjectsOfType<Spawner>();
                foreach (Spawner bottleSpawn in spawners)
                {
                    bottleSpawn.CancelInvoke(); // cancel all spawning processes in the pipeline
                }

                gameStarted = false;

                Invoke("restartGame", 3); //restarts the game after three seconds
            }
        }
        
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    private void restartGame()
    {
        SceneManager.LoadScene(0);
    }
	
}
