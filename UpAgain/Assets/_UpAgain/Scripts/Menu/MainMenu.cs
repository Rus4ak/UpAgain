using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _level;

    private void Start()
    {
        _level.text = $"{PlayerPrefs.GetInt("LastCompletedLevel", 0) + 1} LEVEL";

        LoadingScreen.Instance.Activate(false);
    }

    public void StartGame()
    {
        LoadingScreen.Instance.Activate(true);
        SceneManager.LoadSceneAsync("game");
    }
}
