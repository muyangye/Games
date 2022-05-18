using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private static CanvasScript canvas;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (canvas == null)
        {
            canvas = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
