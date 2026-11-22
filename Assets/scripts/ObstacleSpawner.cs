using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject ghostPrefab;
    public GameObject player;

    public int obstaclesPerRow = 4;
    public float spawnY = 6f;
    public float leftX = -7f;
    public float rightX = 7f;
    public float delayBetweenRows = 1f;
    public float startSpeed = 4f;
    public float speedUpPerRound = 0.3f;

    public int fireOnlyRounds = 3;
    public float ghostMinY = -2f;
    public float ghostMaxY = -2f;
    public float ghostLeftX = -8f;
    public float ghostRightX = 8f;

    private List<GameObject> fireballs = new List<GameObject>();
    private GameObject ghost;
    private float timer;
    private int round;
    private bool stopped = false;

    // called by the timer when the game ends
    public void StopGame()
    {
        stopped = true;

        // remove sekoj fireball && sekoj ghost
        foreach (var fire in FindObjectsByType<Collectable>(FindObjectsSortMode.None))
            Destroy(fire.gameObject);

        foreach (var g in FindObjectsByType<GhostMover>(FindObjectsSortMode.None))
            Destroy(g.gameObject);

        fireballs.Clear();
    }

    void Update()
    {
        // game over
        if (stopped || player == null) return;

        // forget fireballs that were destroyed
        fireballs.RemoveAll(f => f == null);

        // no fireballs left = start the next round after a short delay
        if (fireballs.Count == 0)
        {
            timer += Time.deltaTime;

            if (timer >= delayBetweenRows)
            {
                timer = 0f;
                StartNextRound();
            }
        }
    }

    void StartNextRound()
    {
        round++;
        SpawnFireballs();

        // posle odreden br na rundi(3) se spawna ghost
        if (round > fireOnlyRounds && ghost == null)
            SpawnGhost();
    }

    void SpawnFireballs()
    {
        float speed = startSpeed + speedUpPerRound * (round - 1);
        float slotWidth = (rightX - leftX) / obstaclesPerRow;

        for (int i = 0; i < obstaclesPerRow; i++)
        {
            // random spot u slot za da nema overlap na fireballs
            float x = leftX + slotWidth * i + Random.Range(0.2f, slotWidth - 0.2f);
            Vector3 position = new Vector3(x, spawnY, 0f);

            GameObject fire = Instantiate(obstaclePrefab, position, obstaclePrefab.transform.rotation);
            fire.GetComponent<Collectable>().SetSpeed(speed);
            fireballs.Add(fire);
        }
    }

    void SpawnGhost()
    {
        // za dali da doe od levo ili desno
        float x = Random.value < 0.5f ? ghostLeftX : ghostRightX;
        float y = Random.Range(ghostMinY, ghostMaxY);
        Vector3 position = new Vector3(x, y, 0f);

        ghost = Instantiate(ghostPrefab, position, ghostPrefab.transform.rotation);
        ghost.GetComponent<GhostMover>().Setup(ghostLeftX, ghostRightX);
    }
}