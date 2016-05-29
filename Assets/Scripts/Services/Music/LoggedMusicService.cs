using UnityEngine;
using System.Collections;


/// <summary>
/// Wraps a Music Service and writes to log anytime one of the methods is called.
/// </summary>
public class LoggedMusicService : MusicService
{
    /// <summary>Music Service that is being called</summary>
    private MusicService _wrappedService;
    /// <summary>Music Service that is being called</summary>
    public MusicService WrappedService
    {
        get
        {
            return _wrappedService;
        }
    }


    /// <summary>
    /// Constructor provided the Music Service to be wrapped.
    /// </summary>
    /// <param name="pWrappedService">Music Service to wrap</param>
    public LoggedMusicService(MusicService pWrappedService)
    {
        _wrappedService = pWrappedService;
    }


    /// <summary>
    /// Writes on the log which clip is played,
    /// and passes it to the wrapped service to play it
    /// </summary>
    /// <param name="pMusicClip">Clip to play</param>
    public override void Play(AudioClip pMusicClip)
    {
        Log("Playing music: " + pMusicClip.name);
        _wrappedService.Play(pMusicClip);
    }


    /// <summary>
    /// Writes on the log that we are stopping the music,
    /// and calls the method on the wrapped service.
    /// </summary>
    public override void Stop()
    {
        Log("Stopping music");
        _wrappedService.Stop();
    }


    /// <summary>
    /// Writes the given string to the log.
    /// </summary>
    /// <param name="pMessage">Text to log</param>
    private void Log(string pMessage)
    {
        Debug.Log("<color=blue>" + pMessage + "</color>");
    }
}
