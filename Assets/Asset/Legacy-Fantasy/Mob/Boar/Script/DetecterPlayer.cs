using UnityEngine;

public class DetecterPlayer : MonoBehaviour
{
    float direction = 1f;
    public Transform playerPosition;

    public bool chasePlayer = false;

    Sanglier sangliers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sangliers = GetComponentInParent<Sanglier>();
        direction = sangliers.direction;
    }

    // Update is called once per frame
    void Update()
    {
        direction = sangliers.direction;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerIsInFront(collision.transform))
            {
                
                playerPosition = collision.transform;
                chasePlayer = true;
            }

        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (PlayerIsInFront(collision.transform))
            {
                chasePlayer = true;
                playerPosition = collision.transform;
                
            }

        }
    }

     bool PlayerIsInFront(Transform playerTransform)
    {
        if (direction == 1f && playerTransform.position.x > transform.position.x)
        {
            return true;
        }
        else if (direction == -1f && playerTransform.position.x < transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
}
