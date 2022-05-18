using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private bool isLevel4 = false;

    private void LateUpdate()
    {
        if (isLevel4)
        {
            transform.position = target.position + offset;
        }
        else if (target.position.x > leftLimit && target.position.x < rightLimit)
        {
            var pos = transform.position;
            pos.x = target.position.x + offset.x;
            pos.z = target.position.z + offset.z;
            transform.position = pos;
        }
        
    }

}
