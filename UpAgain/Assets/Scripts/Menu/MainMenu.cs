using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _level;

    private void Start()
    {
        _level.text = $"{PlayerPrefs.GetInt("LastCompletedLevel", 0) + 1} LEVEL";

        DataLoader.Instance.Load();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
