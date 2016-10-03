using UnityEngine;

public class AudioData : ScriptableObject
{
    public static AudioManager audioManager;

    public static void PlaySound(SoundHandle handle)
    {
        audioManager.PlaySound(handle);
    }

    public static void SetSoundParameter(SoundParameterHandle handle, float value, GameObject obj)
    {
        audioManager.SetSoundParameter(handle, value, obj);
    }

    public static void PlaySound(SoundHandle handle, GameObject obj)
    {
        audioManager.PlaySound(handle, obj);
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
