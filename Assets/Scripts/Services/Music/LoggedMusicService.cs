using UnityEngine;
using System.Collections;

public class LoggedMusicService : MusicService
{
    MusicService _wrappedService;
    public MusicService WrappedService
    {
        get
        {
            return _wrappedService;
        }
    }


    public LoggedMusicService(MusicService pWrappedService)
    {
        _wrappedService = pWrappedService;
    }


    public override void Play(AudioClip pMusicClip)
    {
        Log("Playing music: " + pMusicClip.name);
        _wrappedService.Play(pMusicClip);
    }


    public override void Stop()
    {
        Log("Stopping music");
        _wrappedService.Stop();
    }


    private void Log(string pMessage)
    {
        Debug.Log("<color=blue>" + pMessage + "</color>");
    }
}
