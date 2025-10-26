using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;

    public AudioClip backgroundMusic;

    private void Start()
    {
        MusicSource.clip = backgroundMusic;
        MusicSource.Play();
    }

}
