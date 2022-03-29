using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public bool destroyed = false;
    private SpriteRenderer spRenderer;
    public Sprite[] animSprites;
    private int index;
    public float waitTime;

    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("UpdateSprite");
    }

    // Animate the Enemy Bullet as it is moving
    IEnumerator UpdateSprite()
    {
        while (true)
        {
            if (index >= animSprites.Length) index = 0;
            spRenderer.sprite = animSprites[index];
            index++;
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Move the Enemy Bullet downward
    void Update()
    {
        gameObject.transform.position += direction * speed * Time.deltaTime;
        if (gameObject.transform.position.y < -7)
        {
            Destroy(gameObject);
        }
    }

    // Destroy Player and Bunker when in contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Bunker" || other.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }


    }


}
