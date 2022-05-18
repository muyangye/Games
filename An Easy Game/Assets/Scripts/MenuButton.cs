using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{ 
    public int sceneAdd;
    public GameObject topBar;
    public GameObject deathScreen;
    public GameManager gameManager;
    public GameObject GameManager;
    public GameObject canvas;

    private void Start()
    {
        //buttonImage = gameObject.GetComponent<Image>();
    }

    //public void ChangeWhenEnter()
    //{
    //    // Change color to green upon hovering
    //    buttonImage.color = new Color(174F, 163F, 8F);

    //}

    //public void ChangeWhenLeave()
    //{
    //    buttonImage.color = new Color(126F, 118F, 7F);

    //}


    public void StartGame()

    {
        // The sceneAdd variable is changed depends on which scene the button leads to
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneAdd);
        Destroy(GameManager);
        Destroy(canvas);

    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneAdd);
        gameManager.playerDead = false;
        gameManager.NewLevelLoaded();
    }

}
