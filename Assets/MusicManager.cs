using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] MusicPieces;
    public int currentMusicID = 0;
    public AudioSource AudioSource;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (!AudioSource.isPlaying)
        {
            currentMusicID++;
            if (currentMusicID > MusicPieces.Length)
                currentMusicID = 0;

            AudioSource.clip = MusicPieces[currentMusicID];
            AudioSource.Play();
        }
    }
}
