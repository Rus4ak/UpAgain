using System;
using UnityEngine;

public class SettingsValues
{
    private static SettingsValues _instance;

    // Implementation of the Singleton pattern
    public static SettingsValues Instance
    {
        get
        {
            _instance ??= new SettingsValues();

            return _instance;
        }
    }

    public float soundVolume = .7f;
    public float musicVolume = .7f;
    public bool VFX = true;

    public event Action OnChange;

    public void Save()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("VFX", VFX ? 1 : 0);

        OnChange?.Invoke();
    }
}
