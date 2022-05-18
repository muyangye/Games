using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spRenderer;
    public GameManager gameManager;
    public GameObject GameManager;
    public GameObject canvas;

    public Animator animator;
    public bool deadActions = false;


    private bool facingForward = true;
    public bool isDead = false;
    private bool dialogCheck = false;
    NPCShowDialog npcShowDialog = null;


    [SerializeField] private float moveVelocity;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Text coinCount;
    [SerializeField] private Text goalCount;
    [SerializeField] private Text showSignText;
    [SerializeField] private bool isLevel1 = false;
    [SerializeField] private bool isLevel2 = false;
    [SerializeField] private bool isLevel3 = false;
    [SerializeField] private bool isLevel4 = false;
    private int numCoins = 0;
    private int numGoal = 20;
    private int deathHeight = -10;
    private bool showSignBool = false;
    private Transform tf;

    private void Awake()
    {
        Time.timeScale = 1;
        if (isLevel4) deathHeight = -80;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        DontDestroyOnLoad(gameManager);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {
        if (isLevel4)
        {
            if (numCoins >= numGoal) numGoal *= 2;
            goalCount.text = "Goal: " + numGoal;
            coinCount.text = "Current: " + numCoins;
        }
        if (transform.position.y < deathHeight)
        {
            isDead = true;

        }

        CheckDeath();
        if (isLevel1) showSignBool = tf.position.x < -6.5 && tf.position.x > -7.5 ? true : false;
        else if (isLevel2) showSignBool = tf.position.x > -3.2 && tf.position.x < -2.2 ? true : false;
        if (showSignText)showSignText.text = showSignBool ? "[PRESS F]" : "";
        if (Input.GetKeyDown(KeyCode.F) && dialogCheck)
        {
            npcShowDialog.Show();
            dialogCheck = false;
        }
        if (IsGrounded())
        {
            if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.W)))
            {
                animator.SetBool("jumping", true);

                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            }

            if (rb.velocity.y == 0)
            {
                animator.SetBool("jumping", false);
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (rb.velocity.y != 0)
            {
                animator.SetBool("jumping", true);
                animator.SetBool("walking", false);
            }
            else
            {
                animator.SetBool("walking", true);
            }

            rb.velocity = new Vector2(-moveVelocity, rb.velocity.y);
            if (facingForward == true)
            {
                var scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;
                facingForward = false;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (rb.velocity.y != 0)
            {
                animator.SetBool("jumping", true);
                animator.SetBool("walking", false);

            }
            else
            {
                animator.SetBool("walking", true);
            }

            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
            if (facingForward == false)
            {
                var scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;
                facingForward = true;
            }
        }
        else
        {
            animator.SetBool("walking", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void CheckDeath()
    {
        if (isDead && !deadActions)
        {
            isDead = false;
            deadActions = true;
            StartCoroutine("DeathActions");
        }

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
        return raycastHit.collider != null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sign"))
        {
            npcShowDialog = other.gameObject.GetComponent<NPCShowDialog>();
            dialogCheck = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            numCoins += 1;
            Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Decor_Statue")
        {
            isDead = true;
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isDead = true;
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            isDead = true;
        }
        if (collision.gameObject.CompareTag("HiddenTile"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (collision.gameObject.CompareTag("TrapTile"))
        {
            collision.gameObject.SetActive(false);
            Debug.Log("hi");
        }
        if (collision.gameObject.CompareTag("Teleport"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(GameManager);
            Destroy(canvas);
        }

        
    }

    IEnumerator DeathActions()
    {
        gameManager.deathTime = gameManager.deathTime + 1;
        gameManager.playerDead = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.2f);
        animator.SetBool("dying", true);
        yield return new WaitForSecondsRealtime(1f);
    }

}
