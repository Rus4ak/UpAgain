using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void Load()
    {
        Bank.Coins = PlayerPrefs.GetInt("Coins", 0);
        SettingsValues.audioVolume = PlayerPrefs.GetFloat("AudioVolume", .7f);
        SettingsValues.musicVolume = PlayerPrefs.GetFloat("MusicVolume", .7f);
        SettingsValues.VFX = PlayerPrefs.GetInt("VFX", 1) == 1;
    }
}
