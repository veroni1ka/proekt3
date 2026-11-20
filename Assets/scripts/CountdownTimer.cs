using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeLeft = 30f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverText;
    public ObstacleSpawner spawner;

    private bool gameEnded = false;

    void Start()
    {
        if (spawner == null)
            spawner = FindFirstObjectByType<ObstacleSpawner>();
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
            // time ran out = player survived
            gameEnded = true;
            timerText.text = "Nice one!";
            spawner.StopGame();
        }
    }

    // called by the fireball or ghost when it hits the player
    public void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;
        gameOverText.SetActive(true);
        spawner.StopGame();
    }
}