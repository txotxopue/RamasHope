using UnityEngine;
using System.Collections;

public class DefaultSFXService : SFXService
{
    public override void Play(AudioSource pAudioSource, AudioClip pSoundClip)
    {
        pAudioSource.clip = pSoundClip;
        pAudioSource.Play();
    }
}
