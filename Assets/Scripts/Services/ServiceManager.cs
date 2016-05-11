using UnityEngine;
using System.Collections;


/// <summary>
/// Class to handle the services through the Inspector.
/// </summary>
public class ServiceManager : MonoBehaviour
{
    [Header("Music")]
    /// <summary>Type of music service (i.e. default music, logged or no music)</summary>
    [Tooltip("Type of music service (i.e. default music, logged or no music)")]
    [SerializeField]
    private EServiceType _musicService;
    /// <summary>Audio source for the music to play on</summary>
    [Tooltip("Audio source for the music to play on")]
    [SerializeField]
    private AudioSource _defaultMusicAudioSource;

    [Header("SFX")]
    /// <summary>Type of sound effects service (i.e. default sounds, logged or no sounds)</summary>
    [Tooltip("Type of sound effects service (i.e. default sounds, logged or no sounds)")]
    [SerializeField]
    private EServiceType _sfxService;


    // Use this for initialization
    void Awake ()
    {
        // Register on the service locator the Audio Source provided on the Inspector
        ServiceLocator._defaultMusicAudioSource = _defaultMusicAudioSource;

        // Create and register the music service as stated on the enum
	    switch (_musicService)
        {
            case EServiceType.Default:
                // Just a regular music service
                ServiceLocator.ProvideMusicService(new DefaultMusicService(_defaultMusicAudioSource));
                break;
            case EServiceType.Logged:
                // A logged service wrapping the default one
                ServiceLocator.ProvideMusicService(new LoggedMusicService(new DefaultMusicService(_defaultMusicAudioSource)));
                break;
            default:
                // A service that does nothing (muted)
                ServiceLocator.ProvideMusicService(new NullMusicService());
                break;
        }

        // The same thing with the SFX service
        switch (_sfxService)
        {
            case EServiceType.Default:
                ServiceLocator.ProvideSFXService(new DefaultSFXService());
                break;
            case EServiceType.Logged:
                ServiceLocator.ProvideSFXService(new LoggedSFXService(new DefaultSFXService()));
                break;
            default:
                ServiceLocator.ProvideSFXService(new NullSFXService());
                break;
        }
    }
}
