using UnityEngine;
using System.Collections;

public enum SoundHandle
{
    FallDeath,
    PieCharge,
    Death,
    CoffeeEnterScene, // called "CoffeEnterScene" in Soundbank
    TomatoFall,
    PlaceTile,
    CherryFuse,
    CherryExplosion,
    Jump,
    PowerUp,
    TomatoSplat
}

public enum SoundParameterHandle
{
    Distance
}

public class AudioManager : MonoBehaviour
{
    //private GameObject playerCharacter { get; set; }
    private AkBank soundBank;

    void Start()
    {
        soundBank = GetComponent<AkBank>();
        AudioData.audioManager = this;
    }

    public void UnloadBank()
    {
        soundBank.UnloadBank(gameObject);
    }

    public void SetSoundParameter(SoundParameterHandle handle, float value, GameObject obj)
    {
        switch (handle) {
            case SoundParameterHandle.Distance:
                AkSoundEngine.SetRTPCValue("distanceToCharacter", value, obj); break;
            default:
                throw new System.Exception("Enum variant doesn't exist, update SetSoundParameter method (AudioManager.cs)");
        }
    }

    public void PlaySound(SoundHandle handle, GameObject obj)
    {
        switch (handle) {
            case SoundHandle.Jump:
                AkSoundEngine.PostEvent("jump", gameObject); break;
            case SoundHandle.PowerUp:
                AkSoundEngine.PostEvent("powerUp", gameObject); break;
            case SoundHandle.PlaceTile:
                AkSoundEngine.PostEvent("placeTile", gameObject); break;
            case SoundHandle.Death:
                AkSoundEngine.PostEvent("death", gameObject); break;
            case SoundHandle.FallDeath:
                AkSoundEngine.PostEvent("fallDeath", gameObject); break;
            case SoundHandle.PieCharge:
                AkSoundEngine.PostEvent("PieCharge", gameObject); break;
            case SoundHandle.CoffeeEnterScene:
                AkSoundEngine.PostEvent("coffeEnterScene", gameObject); break;
            case SoundHandle.TomatoFall:
                AkSoundEngine.PostEvent("tomatoFall", gameObject); break;
            case SoundHandle.TomatoSplat:
                AkSoundEngine.PostEvent("tomatoSplat", gameObject); break;
            case SoundHandle.CherryFuse:
                AkSoundEngine.PostEvent("cherryFuse", gameObject); break;
            case SoundHandle.CherryExplosion:
                AkSoundEngine.PostEvent("cherryExplosion", gameObject); break;
            default:
                throw new System.Exception("Enum variant doesn't exist, update PlaySound method (AudioManager.cs)");
        }
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
}
