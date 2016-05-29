using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class defining the methods of a MusicService.
/// Also needs an Audio Source to play the music.
/// </summary>
public abstract class MusicService
{
    /// <summary>Audio source from the scene to play the music on</summary>
    public AudioSource _source;


    /// <summary>
    /// Plays the given clip.
    /// </summary>
    /// <param name="pMusicClip">Music clip to play</param>
    public abstract void Play(AudioClip pMusicClip);


    /// <summary>
    /// Stops playing music.
    /// </summary>
    public abstract void Stop();
}
