using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFO : MonoBehaviour
{
    // The UFO is a randomly generated monster that slides across the top of the screen.
	// It gives large amount of points in reward.

    public float speed = 3f;
    public Text scoreNumber;
    public bool destroyed = false;
    private SpriteRenderer spRenderer;
    public Sprite[] scoreSprites;
    private AudioSource killedAudio;
    public GameManager gameManager;
    private int cases = 3; // for one third chance of obtaining random score

    private IVPositions ivPositions;


    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        killedAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ivPositions = GameObject.Find("InvaderPositions").GetComponent<IVPositions>();

    }


    void Update()
    {
        if (ivPositions.active == true)
        {
            moveUFO();
        }
       
        
        if (gameObject.transform.position.x < -6)
        {
            destroyUFO();
        }
    }

    void moveUFO()
    {
        gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void destroyUFO()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "PlayerBullet")
        {
            // We want to play the death sound so we need to destroy this invader 0.3 seconds later
            speed = 0;
            Destroy(gameObject, 0.3f);

            int randNum = Random.Range(0, cases);

            spRenderer.color = Color.white;
            // Randomly add 50, 100, or 200 points 
            if (randNum == 0)
            {
                gameManager.score = gameManager.score + 50;
                spRenderer.sprite = scoreSprites[0];
            } else if (randNum == 1)
            {
                gameManager.score = gameManager.score + 100;
                spRenderer.sprite = scoreSprites[1];
            } else if (randNum == 2)
            {
                gameManager.score = gameManager.score + 150;
                spRenderer.sprite = scoreSprites[2];
            }


            
            killedAudio.Play();
        }
    }

    
}
