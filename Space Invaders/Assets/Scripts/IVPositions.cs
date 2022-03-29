using UnityEngine;
using System.Collections;


public class IVPositions : MonoBehaviour
{

    // Define invaders
    public Invader bottom;
    public Invader middle;
    public Invader top;

    // Define rows and columns of Invaders
    int rows = 5;
    int cols = 11;

    // Define Invaders direction and movement speed 
    public Vector3 direction;
    public float speed;

    // Define Invaders movement frequency
    public float moveFrequency = 1f;

    // Define GameManager
    public GameManager gameManager;

    // Define Active State
    public bool active = true;

    public float positionDrop = 0;
    public int scoreForCompletingLevel = 1000;


    void Start()
    {
        // Create the matrix of Invaders
        SpawnInvaders();
        StartCoroutine("UpdatePosition");
        
    }

    // Move the invaders in a discrete fashion, similar to the movement in the original game
    IEnumerator UpdatePosition()
    {
        while (active == true)
        {
            transform.position += direction * speed;
            yield return new WaitForSeconds(moveFrequency);
        }
    }


    void SpawnInvaders()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 position = new Vector3(0.0f, row * 0.56f - positionDrop, 0.0f);
                position.x += col * 0.56f;
                Invader invader;
                if (row == 0 || row == 1)
                {
                    invader = Instantiate(bottom, transform);
                }
                else if (row == 2 || row == 3)
                {
                    invader = Instantiate(middle, transform);
                }
                else
                {
                    invader = Instantiate(top, transform);
                }
                invader.transform.localPosition = position - new Vector3(2.8f, 0.0f, 0.0f);
            }
        }
    }

    // Check to see if invaders hit the edge and if so, move them down
    void Update()
    {
        foreach (Transform invader in transform)
        {
            if (invader.position.x <= -5.2)
            {
                direction *= -1;
                transform.position = transform.position + new Vector3(0f, -0.24f, 0f);
                transform.position += direction * speed;
                moveFrequency = moveFrequency - 0.1f;

                // Update each individual invader's sprite animation frequency
                foreach (Transform invaderTransform in transform)
                {
                    invaderTransform.gameObject.GetComponent<Invader>().invaderMoveFrequency -= 0.1f;
                }

                    break;

            }
            else if (invader.position.x >= 5.2)
            {
                direction *= -1;
                transform.position = transform.position + new Vector3(0f, -0.24f, 0f);
                transform.position += direction * speed;
                moveFrequency = moveFrequency - 0.1f;

                // Update each individual invader's sprite animation frequency
                foreach (Transform invaderTransform in transform)
                {
                    invaderTransform.gameObject.GetComponent<Invader>().invaderMoveFrequency -= 0.1f;
                }
                break;
            }

            else if (invader.position.y <= -3f)
            {
                gameManager.life = 0;
                gameManager.UpdateLife();

            }
        }

        if (transform.childCount == 0)
        {
            if (gameManager.life < 3)
            {
                gameManager.life += 1;
                gameManager.UpdateLifeIcon();
            }
            StartCoroutine("Reset");
            SpawnInvaders();
        }
    }

    IEnumerator Reset()
    {
        positionDrop += 0.3f;
        gameManager.score += scoreForCompletingLevel;
        scoreForCompletingLevel += 500;
        transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        moveFrequency = 1f;
        yield return new WaitForSeconds(1f);

    }
}
