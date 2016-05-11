using UnityEngine;
using System.Collections;


/// <summary>
/// Static class that gives access to common services like Music or SFX.
/// This services are encapsulated through this Locator so we can have different and swappable implementations.
/// </summary>
public static class ServiceLocator
{
    /// <summary>Active music service</summary>
    private static MusicService _musicService;
    /// <summary>Active sound effect service</summary>
    private static SFXService _sfxService;
    /// <summary>designated 2D Audio Source for the music</summary>
    public static AudioSource _defaultMusicAudioSource;


    /// <summary>
    /// Returns the current active Music Service.
    /// </summary>
    /// <returns>The active Music Service</returns>
    public static MusicService GetMusicService()
    {
        return _musicService;
    }


    /// <summary>
    /// Sets a new Music Service as the active one.
    /// </summary>
    /// <param name="pMusicService">Music Service to register as currently active</param>
    public static void ProvideMusicService(MusicService pMusicService)
    {
        if (pMusicService != null)
        {
            _musicService = pMusicService;
        }
        else
        {
            // If it is null, we provide a new Music Service that does nothing
            _musicService = new NullMusicService();
        }
    }


    /// <summary>
    /// Returns the current active SFX Service.
    /// </summary>
    /// <returns>The active SFX Service</returns>
    public static SFXService GetSFXService()
    {
        return _sfxService;
    }


    /// <summary>
    /// Sets a new SFX Service as the active one.
    /// </summary>
    /// <param name="pSFXService">SFX Service to register as currently active</param>
    public static void ProvideSFXService(SFXService pSFXService)
    {
        if (pSFXService != null)
        {
            _sfxService = pSFXService;
        }
        else
        {
            // If it is null, we provide a new SFX Service that does nothing
            _sfxService = new NullSFXService();
        }
    }


    /// <summary>
    /// Wraps the current Music Service into one that logs the activity.
    /// </summary>
    public static void EnableMusicLogging()
    {
        // We don't wrap it again if it is already wrapped
        if (GetMusicService() as LoggedMusicService == null)
        {
            MusicService loggedService = new LoggedMusicService(GetMusicService());
            ProvideMusicService(loggedService);
        }  
    }


    /// <summary>
    /// Unwraps the current Logged Music Service.
    /// </summary>
    public static void DisableMusicLogging()
    {
        // Don't unwrap if it's not a Logged one
        LoggedMusicService service = GetMusicService() as LoggedMusicService;
        if (service != null)
        {
            ProvideMusicService(service.WrappedService);
        }
    }


    /// <summary>
    /// Wraps the current SFX Service into one that logs the activity.
    /// </summary>
    public static void EnableSFXLogging()
    {
        // We don't wrap it again if it is already wrapped
        if (GetSFXService() as LoggedSFXService == null)
        {
            SFXService loggedService = new LoggedSFXService(GetSFXService());
            ProvideSFXService(loggedService);
        }
    }


    /// <summary>
    /// Unwraps the current Logged SFX Service.
    /// </summary>
    public static void DisableSFXLogging()
    {
        // Don't unwrap if it's not a Logged one
        LoggedSFXService service = GetSFXService() as LoggedSFXService;
        if (service != null)
        {
            ProvideSFXService(service.WrappedService);
        }
    }
}
