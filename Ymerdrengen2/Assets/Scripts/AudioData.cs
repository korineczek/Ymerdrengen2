using UnityEngine;

public class AudioData : ScriptableObject
{
    public static AudioManager audioManager;

    void Awake()
    {

        audioManager = GameObject.Find("Managers").GetComponent<AudioManager>();
    }
    
    public static void PlaySound(SoundHandle handle)
    {
        audioManager.PlaySound(handle);
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
