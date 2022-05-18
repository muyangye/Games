using UnityEngine;

public class HiddenPlatform : MonoBehaviour
{
    private SpriteRenderer spRenderer;
    private void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        spRenderer.enabled = true;
    }
}
