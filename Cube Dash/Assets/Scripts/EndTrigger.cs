using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    
    void OnTriggerEnter()
    {
        FindObjectOfType<GameManager>().CompleteLevel();
    }

}
