using UnityEngine;
using System.Collections;


/// <summary>
/// A regular Music service that can play and stop a music clip.
/// </summary>
public class DefaultMusicService : MusicService
{
    /// <summary>
    /// Constructor assigning the default Audio Source on the scene.
    /// </summary>
    /// <param name="pAudioSource">Audio Source to play the music on</param>
    public DefaultMusicService(AudioSource pAudioSource)
    {
        _source = pAudioSource;
    }


    /// <summary>
    /// Plays the given clip.
    /// </summary>
    /// <param name="pMusicClip">Music clip to play</param>
    public override void Play(AudioClip pMusicClip)
    {
        _source.clip = pMusicClip;
        _source.Play();
    }


    /// <summary>
    /// Stops playing music.
    /// </summary>
    public override void Stop()
    {
        _source.Stop();
    }
}
