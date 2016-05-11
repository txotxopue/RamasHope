using UnityEngine;
using System.Collections;

/// <summary>
/// Muted music service that does nothing.
/// </summary>
public class NullMusicService : MusicService
{
    public override void Play(AudioClip pAudioClip)
    {

    }


    public override void Stop()
    {

    }
}
