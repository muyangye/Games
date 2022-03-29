using UnityEngine;

public class Weapon : MonoBehaviour
{
    // This script is for the player bullet. The player bullet has a tag "PlayerBullet" which \
	// enables it to kill monsters.

    public Vector3 direction;
    public float speed;
    public bool destroyed = false;

    void Update()
    {
        // Move the bullet upward
        gameObject.transform.position += direction * speed * Time.deltaTime;
        if (gameObject.transform.position.y >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

}
