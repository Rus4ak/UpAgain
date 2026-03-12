using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider _sound;
    [SerializeField] private Slider _music;
    [SerializeField] private Toggle _VFX;

    private void Start()
    {
        _sound.value = SettingsValues.Instance.soundVolume;
        _music.value = SettingsValues.Instance.musicVolume;
        _VFX.isOn = SettingsValues.Instance.VFX;
    }

    public void Save()
    {
        SettingsValues.Instance.soundVolume = _sound.value;
        SettingsValues.Instance.musicVolume = _music.value;
        SettingsValues.Instance.VFX = _VFX.isOn;

        SettingsValues.Instance.Save();
    }
}
