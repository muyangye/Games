using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    // This script is for a button prefab that changes its color upon hovering. 

    public Text theText;
    public int sceneAdd;

    public void ChangeWhenEnter()
    {
        // Change color to green upon hovering
        theText.color = new Color(0F, 255F, 0);
    }

    public void ChangeWhenLeave()
    {
        theText.color = Color.white; 

    }


    public void StartGame()

    {
        // The sceneAdd variable is changed depends on which scene the button leads to
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneAdd);
    }

}
