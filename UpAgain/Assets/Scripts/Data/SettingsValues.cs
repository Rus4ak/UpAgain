using UnityEngine;

public class SettingsValues
{
    public static float audioVolume = .7f;
    public static float musicVolume = .7f;
    public static bool VFX = true;

    public static void Save()
    {
        PlayerPrefs.SetFloat("AudioVolume", audioVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("VFX", VFX ? 1 : 0);
    }
}
