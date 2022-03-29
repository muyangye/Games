using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerPart : MonoBehaviour
{
    private int hitCount = 0;

    private void Start()
    {
        var cubeRenderer = GetComponent<Renderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Invader1" || other.tag == "Invader2" || other.tag == "Invader3")
        {
            Destroy(gameObject);
            return;
        }
        hitCount += 1;
        if (hitCount == 2)
        {
            Destroy(gameObject);
        }
        Color damagedColor = new Color32(0, 255, 0, 127);
        gameObject.GetComponent<Renderer>().material.color = damagedColor;
    }

}
