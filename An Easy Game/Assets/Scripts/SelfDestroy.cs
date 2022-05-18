
using System.Collections;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(DestroySelf());
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
