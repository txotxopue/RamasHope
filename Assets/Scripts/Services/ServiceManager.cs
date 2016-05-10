using UnityEngine;
using System.Collections;

public class ServiceManager : MonoBehaviour
{
    [Header("Music")]
    /// <summary>Type of music service (i.e. default music, logged or no music)</summary>
    [SerializeField]
    private EServiceType _musicService;
    /// <summary>Audio source for the music to play on</summary>
    [SerializeField]
    private AudioSource _defaultMusicAudioSource;

    [Header("SFX")]
    /// <summary>Type of sound effects service (i.e. default sounds, logged or no sounds)</summary>
    [SerializeField]
    private EServiceType _sfxService;


    // Use this for initialization
    void Awake ()
    {
        ServiceLocator._defaultMusicAudioSource = _defaultMusicAudioSource;
	    switch (_musicService)
        {
            case EServiceType.Default:
                ServiceLocator.ProvideMusicService(new DefaultMusicService(_defaultMusicAudioSource));
                break;
            case EServiceType.Logged:
                ServiceLocator.ProvideMusicService(new LoggedMusicService(new DefaultMusicService(_defaultMusicAudioSource)));
                break;
            default:
                ServiceLocator.ProvideMusicService(new NullMusicService());
                break;
        }

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
