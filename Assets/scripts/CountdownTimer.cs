using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeLeft = 60f;
    bool printed = false;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Debug.Log(Mathf.FloorToInt(timeLeft));
        }
        else if (!printed)
        {
            Debug.Log("Time's up!");
            printed = true;
        }
    }
}