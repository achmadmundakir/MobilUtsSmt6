using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioSource music;


    void Start()
    {
        music.Play();
    }


    public void StopMusic()
    {
        music.Stop();
    }
}