using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;   // FireObstacle
    public GameObject ghostPrefab;      // Ghost
    public GameObject player;           // PlayerRoot

    public int obstaclesPerRow = 4;
    public float spawnY = 6f;           // height where fire appears
    public float leftX = -7f;           // fire appears from here...
    public float rightX = 7f;           // ...to here
    public float delayBetweenRows = 1f;

    public float startSpeed = 4f;       // fire speed in round 1
    public float speedUpPerRound = 0.3f;

    public int fireOnlyRounds = 3;      // the ghost joins after this many rounds
    public float ghostMinY = -2f;       // lowest the ghost can appear
    public float ghostMaxY = -2f;        // highest the ghost can appear
    public float ghostLeftX = -8f;      // the ghost walks from here...
    public float ghostRightX = 8f;      // ...to here

    private List<GameObject> fireballs = new List<GameObject>();
    private GameObject ghost;
    private float timer;
    private int round;
    private bool stopped = false;

    // the timer calls this when time is up
    public void StopGame()
    {
        stopped = true;

        // remove EVERY fireball in the scene
        Collectable[] allFire = FindObjectsByType<Collectable>(FindObjectsSortMode.None);
        foreach (Collectable fire in allFire)
        {
            Destroy(fire.gameObject);
        }

        // remove EVERY ghost in the scene
        GhostMover[] allGhosts = FindObjectsByType<GhostMover>(FindObjectsSortMode.None);
        foreach (GhostMover g in allGhosts)
        {
            Destroy(g.gameObject);
        }

        fireballs.Clear();
    }
    void Update()
    {
        // time is up or player is gone = do nothing
        if (stopped || player == null) return;

        // forget fireballs that were removed
        fireballs.RemoveAll(f => f == null);

        // no fireballs left = the player survived this round
        if (fireballs.Count == 0)
        {
            timer += Time.deltaTime;

            if (timer >= delayBetweenRows)
            {
                StartNextRound();
                timer = 0f;
            }
        }
    }

    void StartNextRound()
    {
        round++;
        SpawnFireballs();

        // after the fire-only rounds, add the ghost (only once)
        if (round > fireOnlyRounds && ghost == null)
        {
            SpawnGhost();
        }
    }

    void SpawnFireballs()
    {
        float speed = startSpeed + speedUpPerRound * (round - 1);
        float slotWidth = (rightX - leftX) / obstaclesPerRow;

        for (int i = 0; i < obstaclesPerRow; i++)
        {
            float x = leftX + slotWidth * i + Random.Range(0.2f, slotWidth - 0.2f);

            GameObject fire = Instantiate(
                obstaclePrefab,
                new Vector3(x, spawnY, 0f),
                obstaclePrefab.transform.rotation
            );

            fire.GetComponent<Collectable>().SetSpeed(speed);
            fireballs.Add(fire);
        }
    }

    void SpawnGhost()
    {
        float x = Random.Range(ghostLeftX, ghostRightX);
        float y = Random.Range(ghostMinY, ghostMaxY);

        ghost = Instantiate(
            ghostPrefab,
            new Vector3(x, y, 0f),
            ghostPrefab.transform.rotation
        );

        ghost.GetComponent<GhostMover>().Setup(ghostLeftX, ghostRightX);
    }
}