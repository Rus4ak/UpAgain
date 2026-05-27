using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _level;

    private void Start()
    {
        Initialize();

        LoadingScreen.Instance.Activate(false);
    }

    private void Initialize()
    {
        _level.text = $"{LevelsData.completedLevelsMap[Map.CurrentMap] + 1} LEVEL";
    }

    private void OnEnable()
    {
        Map.MapChanging += Initialize;
    }

    private void OnDisable()
    {
        Map.MapChanging -= Initialize;
    }

    public void StartGame()
    {
        LoadingScreen.Instance.Activate(true);
        SceneManager.LoadSceneAsync("Game");
    }
}
