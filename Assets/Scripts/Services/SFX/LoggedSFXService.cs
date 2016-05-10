using UnityEngine;
using System.Collections;

public class LoggedSFXService : SFXService
{
    SFXService _wrappedService;
    public SFXService WrappedService
    {
        get
        {
            return _wrappedService;
        }
    }

    public LoggedSFXService(SFXService pWrappedService)
    {
        _wrappedService = pWrappedService;
    }



    public override void Play(AudioSource pAudioSource, AudioClip pSoundClip)
    {
        Log("Playing sound: " + pSoundClip.name);
        _wrappedService.Play(pAudioSource, pSoundClip);
    }


    private void Log(string pMessage)
    {
        Debug.Log("<color=blue>" + pMessage + "</color>");
    }
}
