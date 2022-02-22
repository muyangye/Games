// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Use this if want first-person view: transform.position = player.position;
        transform.position = player.position + offset;
    }
}
