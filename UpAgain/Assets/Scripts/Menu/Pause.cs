using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void SetTimeScale(int number)
    {
        Time.timeScale = number;

        GameManager.Instance.playerMovement.SetMove(number == 1);
    }

    public void LoadMainMenu()
    {
        SetTimeScale(1);
        SceneManager.LoadScene("MainMenu");
        Destroy(Music.Instance.gameObject);
    }
}
