using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float speed = 1.5f;
    public Weapon laserPrefab;
    private Weapon fired;
    private AudioSource fireAudio;
    public GameManager gameManager;


    public Sprite[] animSprites;
    private int index;
    private SpriteRenderer spRenderer;

    private IVPositions ivPositions;




    void Start()
    {
        fireAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ivPositions = GameObject.Find("InvaderPositions").GetComponent<IVPositions>();
        spRenderer = GetComponent<SpriteRenderer>();

    }

    void MoveLeft()
    {
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position +=  Vector3.left * speed * Time.deltaTime;
        }
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void Update()
    {
        if (ivPositions.active == true)
        {
            // If the player touches the left boundary
            if (transform.position.x <= -5.2)
            {
                // Can only move right
                MoveRight();
            }
            // If the player touches the right boundary
            else if (transform.position.x >= 5.2)
            {
                // Can only move left
                MoveLeft();
            }
            else
            {
                MoveLeft();
                MoveRight();
            }
            // Fire!!!
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fire();
            }

        }


    }

    IEnumerator UpdateSprite()
    {
        while (true)
        {
            if (index >= animSprites.Length) index = 0;
            spRenderer.sprite = animSprites[index];
            index++;
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator PlayerHit()
    {
        // We want to play the death sound so we need to destroy this invader 0.4 seconds later
        ivPositions.active = false; 
        StartCoroutine("UpdateSprite");

        yield return new WaitForSeconds(1f);
        


        Destroy(gameObject);


        // But we need to make it look like death, so we disable the sprite renderer
        gameManager.life = gameManager.life - 1;
        gameManager.UpdateLife();
        Debug.Log(ivPositions);
        ivPositions.active = true;
        ivPositions.StartCoroutine("UpdatePosition");

        var invaders = FindObjectsOfType<Invader>();
        foreach (Invader invaderUnit in invaders)
        {
            invaderUnit.StartCoroutine("UpdateSprite");
        }

    }

    private void fire()
    {
        // The player can only fire when the last laser fired was destroyed (hit or go beyond the scene)
        // or it is the first time firing, need to check null first to avoid null has no attribute destroyed error
        if (fired == null)
        {
            fireAudio.Play();
            fired = Instantiate(laserPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            StartCoroutine("PlayerHit");
        }
    }
}
