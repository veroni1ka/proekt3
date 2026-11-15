using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject player;

    public int obstaclesPerRow = 4;
    public float spawnY = 6f;
    public float leftX = -7f;
    public float rightX = 7f;
    public float delayBetweenRows = 1f;

    private List<GameObject> currentRow = new List<GameObject>();
    private float timer;
    private bool gameOver;

    private void Update()
    {
        // player dies=game over
        if (player == null)
        {
            if (!gameOver)
            {
                gameOver = true;
                Debug.Log("GAME OVER");
            }
            return;
        }

        // remove obstacles that fell off
        currentRow.RemoveAll(o => o == null);

        // ako prvote obstacles se gone a playerot e zhiv togash dr obstacles se spawnaat
        if (currentRow.Count == 0)
        {
            timer += Time.deltaTime;

            if (timer >= delayBetweenRows)
            {
                SpawnRow();
                timer = 0f;
            }
        }
    }

    private void SpawnRow()
    {
        float slotWidth = (rightX - leftX) / obstaclesPerRow;

        for (int i = 0; i < obstaclesPerRow; i++)
        {
            // one obstacle per slot, randomly placed 
            float x = leftX + slotWidth * i + Random.Range(0.2f, slotWidth - 0.2f);

            GameObject obstacle = Instantiate(
                obstaclePrefab,
                new Vector3(x, spawnY, 0f),
                Quaternion.identity
            );

            currentRow.Add(obstacle);
        }
    }
}