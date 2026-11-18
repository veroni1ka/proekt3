using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject obstaclePrefab;   // FireObstacle prefab
    public GameObject ghostPrefab;      // Ghost prefab
    public GameObject player;           // PlayerRoot

    [Header("Fire")]
    public int obstaclesPerRow = 4;
    public float spawnY = 6f;
    public float leftX = -7f;
    public float rightX = 7f;
    public float delayBetweenRows = 1f;
    public float baseFallSpeed = 4f;          // speed in round 1
    public float speedIncreasePerRound = 0.3f;
    public float maxFallSpeed = 9f;

    [Header("Ghosts")]
    public int fireOnlyRounds = 3;     // ghosts start AFTER this many rounds
    public int secondGhostRound = 7;   // from this round on, 2 ghosts
    public float ghostMinY = -2f;
    public float ghostMaxY = 3f;
    public float ghostLifetime = 5f;

    private List<GameObject> currentFire = new List<GameObject>();
    private float timer;
    private int roundNumber;
    private bool gameOver;

    private void Update()
    {
        if (player == null)
        {
            if (!gameOver)
            {
                gameOver = true;
                Debug.Log("GAME OVER - you survived " + roundNumber + " rounds");
            }
            return;
        }

        currentFire.RemoveAll(o => o == null);

        // all fire gone and player alive = round survived
        if (currentFire.Count == 0)
        {
            timer += Time.deltaTime;

            if (timer >= delayBetweenRows)
            {
                SpawnRound();
                timer = 0f;
            }
        }
    }

    private void SpawnRound()
    {
        roundNumber++;

        SpawnFireRow();

        if (roundNumber > fireOnlyRounds)
        {
            int ghostCount = roundNumber >= secondGhostRound ? 2 : 1;
            SpawnGhosts(ghostCount);
        }
    }

    private void SpawnFireRow()
    {
        float speed = Mathf.Min(
            baseFallSpeed + speedIncreasePerRound * (roundNumber - 1),
            maxFallSpeed
        );

        float slotWidth = (rightX - leftX) / obstaclesPerRow;

        for (int i = 0; i < obstaclesPerRow; i++)
        {
            float x = leftX + slotWidth * i + Random.Range(0.2f, slotWidth - 0.2f);

            GameObject fire = Instantiate(
                obstaclePrefab,
                new Vector3(x, spawnY, 0f),
                obstaclePrefab.transform.rotation
            );

            Collectable c = fire.GetComponent<Collectable>();
            if (c != null) c.SetSpeed(speed);

            currentFire.Add(fire);
        }
    }

    private void SpawnGhosts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(leftX, rightX);
            float y = Random.Range(ghostMinY, ghostMaxY);

            GameObject ghost = Instantiate(
                ghostPrefab,
                new Vector3(x, y, 0f),
                ghostPrefab.transform.rotation
            );

            GhostMover mover = ghost.GetComponent<GhostMover>();
            if (mover != null) mover.Setup(leftX, rightX, ghostLifetime);
            // ghosts are NOT added to currentFire, so they don't delay the next round
        }
    }
}