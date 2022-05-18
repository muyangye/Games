using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCount : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    private void LateUpdate()
    {
        transform.position = cameraTransform.position + new Vector3(8.5f, -1f, 1f);
    }
}
