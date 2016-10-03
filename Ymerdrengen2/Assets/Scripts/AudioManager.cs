using UnityEngine;
using System.Collections;

public enum SoundHandle
{
    Jump,
    PowerUp,
    PlaceTile,
    Death,
    FallDeath,
    PieCharge,
    CoffeeEnterScene,
    CoffeeSip,
    CoffeeSpit,
    TomatoFall,
    TomatoSplat,
    CherryFall,
    CherryFuse,
    CherryExplosion,
    MenuClickForward,
    MenuClickBack
}

public enum StopSoundHandle
{
    Pause,
    PieCharge
}

public enum SoundParameterHandle
{
    MasterVolume,
    MusicVolume,
    Distance
}

public class AudioManager : MonoBehaviour
{
    public bool InMenu = false;

    //private GameObject playerCharacter { get; set; }
    private AkBank soundBank;
    
    void Start()
    {
        soundBank = GetComponent<AkBank>();
        AudioData.audioManager = this;

        if (PlayerPrefs.GetInt("SoundMute") == 1) {
            SetSoundParameter(SoundParameterHandle.MasterVolume, 0);
            return;
        }
        SetSoundParameter(SoundParameterHandle.MasterVolume, PlayerPrefs.GetFloat("SoundVolume"));

        if (InMenu) {
            StartMenuMusic();
        } else {
            StartMusic();
        }
    }

    public void SetSoundParameter(SoundParameterHandle handle, float value, GameObject obj)
    {
        switch (handle) {
            case SoundParameterHandle.Distance:
                AkSoundEngine.SetRTPCValue("distanceToCharacter", value, obj); break;
            case SoundParameterHandle.MasterVolume:
                AkSoundEngine.SetRTPCValue("masterVolume", value, obj); break;
            case SoundParameterHandle.MusicVolume:
                AkSoundEngine.SetRTPCValue("musicVolume", value, obj); break;
            default:
                throw new System.Exception("Enum variant doesn't exist, update SetSoundParameter method (AudioManager.cs)");
        }
    }
    public void SetSoundParameter(SoundParameterHandle handle, float value)
    {
        SetSoundParameter(handle, value, gameObject);
    }

    public void PlaySound(SoundHandle handle, GameObject obj)
    {
        string identifier = string.Empty;
        switch (handle) {
            case SoundHandle.Jump:
                identifier = "jump";  break;
            case SoundHandle.PowerUp:
                identifier = "powerUp"; break;
            case SoundHandle.PlaceTile:
                identifier = "placeTile"; break;
            case SoundHandle.Death:
                identifier = "death"; break;
            case SoundHandle.FallDeath:
                identifier = "fallDeath"; break;
            case SoundHandle.PieCharge:
                identifier = "pieCharge"; break;
            case SoundHandle.CoffeeEnterScene:
                identifier = "coffeEnterScene"; break;
            case SoundHandle.CoffeeSip:
                identifier = "coffeSip"; break;
            case SoundHandle.CoffeeSpit:
                identifier = "coffeSpit"; break;
            case SoundHandle.TomatoFall:
                identifier = "tomatoFall"; break;
            case SoundHandle.TomatoSplat:
                identifier = "tomatoSplat"; break;
            case SoundHandle.CherryFall:
                identifier = "cherryFall"; break;
            case SoundHandle.CherryFuse:
                identifier = "cherryFuse"; break;
            case SoundHandle.CherryExplosion:
                identifier = "cherryExplosion"; break;
            case SoundHandle.MenuClickForward:
                identifier = "menuClickFoward"; break; // Spelling Error.
            case SoundHandle.MenuClickBack:
                identifier = "menuClickBack"; break;
            default:
                throw new System.NotImplementedException("Enum variant doesn't exist, update PlaySound method (AudioManager.cs)");
        }

        AkSoundEngine.PostEvent(identifier, obj);
    }

    public void StopSound(StopSoundHandle handle, GameObject obj)
    {
        string identifier = string.Empty;
        switch (handle) {
            case StopSoundHandle.Pause:
                identifier = "pause"; break;
            case StopSoundHandle.PieCharge:
                identifier = "pieChargeStop"; break;
            default:
                throw new System.NotImplementedException("Enum variant doesn't exist, update StopSound method (AudioManager.cs)");
        }

        AkSoundEngine.PostEvent(identifier, obj);
    }

    public void PlaySound(SoundHandle handle)
    {
        PlaySound(handle, gameObject);
    }

    public void StartMusic()
    {
        AkSoundEngine.PostEvent("musicPlay", gameObject);
    }

    public void StopMusic()
    {
        AkSoundEngine.PostEvent("musicStop", gameObject);
    }
    
    public void StartMenuMusic()
    {
        AkSoundEngine.PostEvent("menuMusicPlay", gameObject);
    }

    public void StopMenuMusic()
    {
        AkSoundEngine.PostEvent("menuMusicStop", gameObject);
    }
}
