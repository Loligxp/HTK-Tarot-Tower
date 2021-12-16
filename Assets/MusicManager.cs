using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] MusicPieces;
    public int currentMusicID = 0;
    public AudioSource AudioSource;

    // Update is called once per frame
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
