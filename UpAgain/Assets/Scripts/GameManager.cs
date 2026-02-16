using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentLevel;

    public int CurrentLevel {  get { return _currentLevel; } }
    public PlayerMovement playerMovement;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        _currentLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0) + 1;
    }
}
