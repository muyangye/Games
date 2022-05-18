using UnityEngine.SceneManagement;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject teleportPortal;
    private Rigidbody2D rb;
    private Animator animator;
    private bool found = false;
    
    private bool isDead = false;
    private int deathHeight = -10;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (found || findPlayer())
        {
            animator.SetBool("walking", true);
            rb.velocity = new Vector2(-1, rb.velocity.y);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (transform.position.y < deathHeight)
        {
            Debug.Log("hi");
            isDead = true;
            teleportPortal.SetActive(true);
             
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attacking", true);
        }
    }
    bool findPlayer()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.left, distance, layerMask);
        if (raycastHit.collider != null) found = true;
        return raycastHit.collider != null;
    }
}
