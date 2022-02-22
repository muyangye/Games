using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;
    int health = 2;

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            GetComponent<AudioSource>().Play();
            health -= 1;
            Destroy(collisionInfo.collider.gameObject);
            if (health <= 0)
            {
                movement.enabled = false;
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

}
