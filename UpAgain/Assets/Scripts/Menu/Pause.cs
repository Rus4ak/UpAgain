using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    public void SetTimeScale(int number)
    {
        Time.timeScale = number;

        GameManager.Instance.playerMovement.SetMove(number == 1);

        if (number == 0)
            _mixer.SetFloat("Audio", -80f);
        else
            _mixer.SetFloat("Audio", 0f);
    }

    public void LoadMainMenu()
    {
        SetTimeScale(1);
        SceneManager.LoadScene("MainMenu");
        Destroy(Music.Instance.gameObject);
    }
}
