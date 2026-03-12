using UnityEngine;

public class DataLoader : MonoBehaviour
{
    private void Awake()
    {
        Bank.Coins = PlayerPrefs.GetInt("Coins", 0);
        SettingsValues.Instance.soundVolume = PlayerPrefs.GetFloat("SoundVolume", .7f);
        SettingsValues.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume", .7f);
        SettingsValues.Instance.VFX = PlayerPrefs.GetInt("VFX", 1) == 1;
    }
}
