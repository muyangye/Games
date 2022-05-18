using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortalLevel4 : MonoBehaviour
{
    public GameObject topBar;
    public GameObject winScreen;

    private static TeleportPortalLevel4 teleportPortal;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (teleportPortal == null)
        {
            teleportPortal = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
        

    }

    void Start()
    {
        topBar = GameObject.Find("TopBar");
        winScreen = GameObject.Find("WinScreen");
        winScreen.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            topBar.SetActive(false);
            winScreen.SetActive(true);
        }
    }
        
}
