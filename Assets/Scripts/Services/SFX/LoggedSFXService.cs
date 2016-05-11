using UnityEngine;
using System.Collections;

/// <summary>
/// Wraps an SFX Service and writes to log anytime one of the methods is called.
/// </summary>
public class LoggedSFXService : SFXService
{
    /// <summary>SFX Service that is being called</summary>
    SFXService _wrappedService;
    /// <summary>SFX Service that is being called</summary>
    public SFXService WrappedService
    {
        get
        {
            return _wrappedService;
        }
    }


    /// <summary>
    /// Constructor provided the SFX Service to be wrapped.
    /// </summary>
    /// <param name="pWrappedService">SFX Service to wrap</param>
    public LoggedSFXService(SFXService pWrappedService)
    {
        _wrappedService = pWrappedService;
    }


    /// <summary>
    /// Writes on the log which clip is played,
    /// and passes it to the wrapped service to play it
    /// </summary>
    /// <param name="pAudioSource">Audio Source to play the sound on</param>
    /// <param name="pSoundClip">Sound clip to play</param>
    public override void Play(AudioSource pAudioSource, AudioClip pSoundClip)
    {
        Log("Playing sound: " + pSoundClip.name);
        _wrappedService.Play(pAudioSource, pSoundClip);
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
