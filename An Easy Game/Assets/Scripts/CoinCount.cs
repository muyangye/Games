using UnityEngine;

public class CoinCount : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    private void LateUpdate()
    {
        transform.position = cameraTransform.position + new Vector3(8.5f, 0f, 1f);
    }
}
