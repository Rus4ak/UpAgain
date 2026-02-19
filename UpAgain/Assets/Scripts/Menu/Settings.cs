using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _audio;
    [SerializeField] private Slider _music;
    [SerializeField] private Toggle _VFX;

    private void Start()
    {
        _audio.value = SettingsValues.audioVolume;
        _music.value = SettingsValues.musicVolume;
        _VFX.isOn = SettingsValues.VFX;
    }

    public void Save()
    {
        SettingsValues.audioVolume = _audio.value;
        SettingsValues.musicVolume = _music.value;
        SettingsValues.VFX = _VFX.isOn;

        SettingsValues.Save();
    }
}
