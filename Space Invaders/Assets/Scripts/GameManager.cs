using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // The player starts with score 0 and life 3.
    public int score = 0;
    public int life = 2;

    // Define UIs
    private GameObject scoreText;
    public GameObject lifeIcon;

    // Define player and life icons
    public GameObject player;
    private GameObject icon1;
    private GameObject icon2;
    private GameObject icon3;

    // Define Endgame UI texts
    private GameObject gameOver;
    private GameObject playAgain;


void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreNumber");
        UpdateLife();
    }

void Update()
    {
        UpdateScoreText();
    }

public void UpdateLifeIcon()
{
    Destroy(icon1);
    Destroy(icon2);
    Destroy(icon3);
    // Show 3 life icons if player has 3 lives
    if (life == 3)
    {
        icon1 = Instantiate(lifeIcon, new Vector3(2.7f, 3.12f, 0), Quaternion.identity);
        icon2 = Instantiate(lifeIcon, new Vector3(3.6f, 3.12f, 0), Quaternion.identity);
        icon3 = Instantiate(lifeIcon, new Vector3(4.5f, 3.12f, 0), Quaternion.identity);
    }

    // Show 2 life icons if player has 2 lives
    if (life == 2)
    {
        icon1 = Instantiate(lifeIcon, new Vector3(2.7f, 3.12f, 0), Quaternion.identity);
        icon2 = Instantiate(lifeIcon, new Vector3(3.6f, 3.12f, 0), Quaternion.identity);
    }

    // Show 1 life icons if player has 1 live
    if (life == 1)
    {
        icon1 = Instantiate(lifeIcon, new Vector3(2.7f, 3.12f, 0), Quaternion.identity);
    }
}

public void UpdateLife()
    {
        UpdateLifeIcon();
        Instantiate(player, new Vector3(0f, -3.2f, 0), Quaternion.identity);

        // Begin Endgame Actions
        if (life == 0)
        {
            Destroy(icon1);
            StartCoroutine("EndGameActions");
        }

    }

void UpdateScoreText()
    {
        scoreText.GetComponent<Text>().text = score.ToString();

    }


IEnumerator EndGameActions()
    {
        // Define and Deactivate Active Parts of the Game
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject bullet in enemyBullets)
        {
            GameObject.Destroy(bullet);
        }

        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }


        // Define and Activate Endgame UIs that show up 
        gameOver = GameObject.Find("/Canvas/GameOver");
        playAgain = GameObject.Find("/Canvas/PlayAgain");

 
        gameOver.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(1f);
        playAgain.transform.GetChild(0).GetComponent<Text>().enabled = true;
        yield break;
    }



}
