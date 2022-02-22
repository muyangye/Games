using UnityEngine;

public class MusicScript : MonoBehaviour
{

    private AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

}
