using UnityEngine;
using System.Collections;

/// <summary>
/// Muted SFX service that does nothing.
/// </summary>
public class NullSFXService : SFXService
{
    public override void Play(AudioSource pAudioSource, AudioClip pSoundClip)
    {

    }
}
