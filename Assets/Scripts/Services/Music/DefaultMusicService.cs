using UnityEngine;
using System.Collections;

public class DefaultMusicService : MusicService
{
    public DefaultMusicService(AudioSource pAudioSource)
    {
        _source = pAudioSource;
    }

    public override void Play(AudioClip pMusicClip)
    { 
        _source.clip = pMusicClip;
        _source.Play();
    }


    public override void Stop()
    {
        _source.Stop();
    }
}
