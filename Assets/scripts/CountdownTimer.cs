using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeLeft = 30f;
    public TextMeshProUGUI timerText;      // the timer on the screen
    public GameObject gameOverText;        // the Game Over text
    public ObstacleSpawner spawner;        // the spawner to stop

    private bool gameEnded = false;

    void Start()
    {
        // if the slot is empty, find the spawner ourselves
        if (spawner == null)
        {
            spawner = FindFirstObjectByType<ObstacleSpawner>();
        }
    }

    void Update()
    {
        if (gameEnded) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft > 0f)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
        }
        else
        {
            // time ran out, the player survived
            gameEnded = true;
            timerText.text = "Time's up!";
            spawner.StopGame();
        }
    }

    // the fireball or ghost calls this when it hits the player
    public void GameOver()
    {
        if (gameEnded) return;   // already over, do nothing

        gameEnded = true;
        gameOverText.SetActive(true);   // show "Game Over"
        spawner.StopGame();             // remove fire and ghost
    }
}