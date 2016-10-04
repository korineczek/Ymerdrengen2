using UnityEngine;

public class AudioData : ScriptableObject
{
    public static AudioManager audioManager;



    public static void SetSoundParameter(SoundParameterHandle handle, float value, GameObject obj)
    {
        audioManager.SetSoundParameter(handle, value, obj);
    }

    public static void SetSoundParameter(SoundParameterHandle handle, float value)
    {
        audioManager.SetSoundParameter(handle, value);
    }

    public static void PlaySound(SoundHandle handle, GameObject obj)
    {
        audioManager.PlaySound(handle, obj);
    }

    public static void PlaySound(SoundHandle handle)
    {
        audioManager.PlaySound(handle);
    }

    public static void StopSound(StopSoundHandle handle, GameObject obj)
    {
        audioManager.StopSound(handle, obj);
    }

    public static void StopSound(StopSoundHandle handle)
    {
        audioManager.StopSound(handle);
    }

    public static void StartMusic()
    {
        audioManager.StartMusic();
    }

    public static void StopMusic()
    {
        audioManager.StopMusic();
    }
}
