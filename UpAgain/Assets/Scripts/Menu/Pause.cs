using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void SetTimeScale(int number)
    {
        Time.timeScale = number;
    }

    public void LoadMainMenu()
    {
        SetTimeScale(1);
        SceneManager.LoadScene("MainMenu");
    }
}
