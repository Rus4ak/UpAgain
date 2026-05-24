using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private string _menuScene;

    private void Awake()
    {
        BankLoad();
        SettingsLoad();
        AbilitiesLoad();
        MapLoad();

        SceneManager.LoadSceneAsync(_menuScene);
    }

    private void BankLoad()
    {
        Bank.Coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void SettingsLoad()
    {
        SettingsValues.Instance.soundVolume = PlayerPrefs.GetFloat("SoundVolume", .7f);
        SettingsValues.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume", .7f);
        SettingsValues.Instance.VFX = PlayerPrefs.GetInt("VFX", 1) == 1;
    }

    private void AbilitiesLoad()
    {
        AbilitiesValues.values[AbilityType.TurboSpeed] = PlayerPrefs.GetInt("TurboSpeed", 3);
        AbilitiesValues.values[AbilityType.Shield] = PlayerPrefs.GetInt("Shield", 3);
        AbilitiesValues.values[AbilityType.Freeze] = PlayerPrefs.GetInt("Freeze", 3);
    }

    private void MapLoad()
    {
        Map.CurrentMap = PlayerPrefs.GetInt("CurrentMap", 0);
    }
}
