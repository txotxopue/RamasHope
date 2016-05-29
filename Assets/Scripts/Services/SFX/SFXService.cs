using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class defining the methods of an SFXService.
/// </summary>
public abstract class SFXService
{
    /// <summary>
    /// Plays the given sound clip on the given audio source.
    /// </summary>
    /// <param name="pAudioSource">Audio Source to play the sound on</param>
    /// <param name="pSoundClip">Sound clip to play</param>
    public abstract void Play(AudioSource pAudioSource, AudioClip pSoundClip);
}
