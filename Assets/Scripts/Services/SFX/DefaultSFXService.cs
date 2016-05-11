using UnityEngine;
using System.Collections;

/// <summary>
/// A regular SFX service that can play a SFX clip.
/// </summary>
public class DefaultSFXService : SFXService
{
    /// <summary>
    /// Plays the given sound clip on the given audio source.
    /// </summary>
    /// <param name="pAudioSource">Audio Source to play the sound on</param>
    /// <param name="pSoundClip">Sound clip to play</param>
    public override void Play(AudioSource pAudioSource, AudioClip pSoundClip)
    {
        pAudioSource.clip = pSoundClip;
        pAudioSource.Play();
    }
}
