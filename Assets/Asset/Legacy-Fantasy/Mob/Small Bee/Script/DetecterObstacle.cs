using UnityEngine;

public class DetecterObstacle : MonoBehaviour
{
    public bool isObstacleDetected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isObstacleDetected = false;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isObstacleDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isObstacleDetected = false;
    }
}
