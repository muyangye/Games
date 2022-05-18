using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public int deathTime = 0;
    public Text pauseScreenText;
    public TMPro.TextMeshProUGUI deathScreenText;
    public Text topBarText;
    public bool playerDead = false;
    public bool deadActions = false;
    public GameObject deathScreen;
    public GameObject topBar;
    public bool isLevel4 = false;



    private static GameManager gameManager;
    void Awake()
    {
        if (!isLevel4)
        {
            DontDestroyOnLoad(this);

            if (gameManager == null)
            {
                gameManager = this;
            }
            else
            {
                DestroyObject(gameObject);
            }
        }
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pauseScreenText.text = deathTime.ToString() + " Times";
        deathScreenText.text = deathTime.ToString();
        topBarText.text = deathTime.ToString();


        if (playerDead && !deadActions)
        {
            deadActions = true;
            StartCoroutine("DeathActions");
            Debug.Log("death actions ran");

        }
    }

    IEnumerator DeathActions() {
        yield return new WaitForSecondsRealtime(1.2f);
        deathScreen.SetActive(true);
        topBar.SetActive(false);

    }

    public void NewLevelLoaded() {
        topBar.SetActive(true);
        deathScreen.SetActive(false);
        deadActions = false;
    }


}
