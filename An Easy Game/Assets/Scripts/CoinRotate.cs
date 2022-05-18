using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 240f * Time.deltaTime, 0f, Space.World);
    }
}
