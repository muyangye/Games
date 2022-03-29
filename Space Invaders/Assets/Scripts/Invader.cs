using UnityEngine;
using System.Collections;

public class Invader : MonoBehaviour
{

    public GameManager gameManager;
    private AudioSource killedAudio;

    // Define sprites and related variables for animation 
    public Sprite[] animSprites;
    private int index;
    private SpriteRenderer spRenderer;

    // Define score earned when the invader is killed. There are three different scores
	// for the three types of invaders
    public int scoreEarned;

    // Define the two different enemy bullets and the chance for the invader to fire
    public int chanceForFire = 200000;
    public GameObject enemyBullet;
    public GameObject enemyBullet2;

    // Reference the IVPosition game object to obtain reference to the moveFrequency variable
    public float invaderMoveFrequency = 1f;

    private IVPositions ivPositions;



    void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        killedAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ivPositions = GameObject.Find("InvaderPositions").GetComponent<IVPositions>();
        StartCoroutine("UpdateSprite");
    }

    // Animation Logic
    IEnumerator UpdateSprite()
    {
        while (ivPositions.active == true)
        {
            if (index >= animSprites.Length) index = 0;
            spRenderer.sprite = animSprites[index];
            index++;
            yield return new WaitForSeconds(invaderMoveFrequency);
        }
        if (ivPositions.active == false)
        {
            ivPositions = GameObject.Find("InvaderPositions").GetComponent<IVPositions>();
            Debug.Log(ivPositions.active);
        }
    }

    void Update()
    {
        if (ivPositions.active == true)
        {
            // If the invader is of type squid, enable it to shoot randomly one of two types of missiles
            if (Random.Range(0, chanceForFire) == 1 && gameObject.tag == "Invader3")
            {
                if (Random.Range(0, 2) == 1)
                {
                    Instantiate(enemyBullet, gameObject.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyBullet2, gameObject.transform.position, Quaternion.identity);
                }
            }
        }
        

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            // We want to play the death sound so we need to destroy this invader 0.3 seconds later
            Destroy(gameObject, 0.3f);
            // scoreText = scoreText + 10
            // But we need to make it look like death, so we disable the sprite renderer
            spRenderer.enabled = false;
            gameManager.score = gameManager.score + scoreEarned;
            killedAudio.Play();
        }
    }

}

