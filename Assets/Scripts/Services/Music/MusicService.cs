using UnityEngine;
using System.Collections;

public abstract class MusicService
{
    public AudioSource _source;

    public abstract void Play(AudioClip pMusicClip);

    public abstract void Stop();
}
