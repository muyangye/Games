using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private float distance;
    private bool found = false;
    [SerializeField] private LayerMask layerMask;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (found || findPlayer())
        {
            rb.gravityScale = 1;
        }
    }

    bool findPlayer()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, distance, layerMask);
        if (raycastHit.collider != null) found = true;
        return raycastHit.collider != null;
    }


}
