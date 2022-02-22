using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        // rb = gameObject.GetComponent<Rigidbody>();
        // rb.AddForce(0, 200, 500);
    }

    // FixedUpdate because we are using it to mess with physics
    void FixedUpdate()
    {
        // Add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if ( Input.GetKey("d") )
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if ( Input.GetKey("a") )
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if ( Input.GetKey(KeyCode.DownArrow) )
        {
            rb.velocity *= 0.9f;
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
