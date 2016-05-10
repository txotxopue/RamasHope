using UnityEngine;
using System.Collections;

public static class ServiceLocator
{
    private static MusicService _musicService;
    private static SFXService _sfxService;

    public static AudioSource _defaultMusicAudioSource;

    public static MusicService GetMusicService()
    {
        return _musicService;
    }

    public static void ProvideMusicService(MusicService pMusicService)
    {
        if (pMusicService != null)
        {
            _musicService = pMusicService;
        }
        else
        {
            _musicService = new NullMusicService();
        }
    }

    public static SFXService GetSFXService()
    {
        return _sfxService;
    }

    public static void ProvideSFXService(SFXService pSFXService)
    {
        if (pSFXService != null)
        {
            _sfxService = pSFXService;
        }
        else
        {
            _sfxService = new NullSFXService();
        }
    }


    public static void EnableMusicLogging()
    {
        if (GetMusicService() as LoggedMusicService == null)
        {
            MusicService loggedService = new LoggedMusicService(GetMusicService());
            ProvideMusicService(loggedService);
        }  
    }

    public static void DisableMusicLogging()
    {
        LoggedMusicService service = GetMusicService() as LoggedMusicService;
        if (service != null)
        {
            ProvideMusicService(service.WrappedService);
        }
    }

    public static void EnableSFXLogging()
    {
        if (GetSFXService() as LoggedSFXService == null)
        {
            SFXService loggedService = new LoggedSFXService(GetSFXService());
            ProvideSFXService(loggedService);
        }
    }

    public static void DisableSFXLogging()
    {
        LoggedSFXService service = GetSFXService() as LoggedSFXService;
        if (service != null)
        {
            ProvideSFXService(service.WrappedService);
        }
    }
}
