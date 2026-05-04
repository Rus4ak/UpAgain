using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ObjectPool _obstaclePool;
    [SerializeField] private ObjectPool _obstacleSmokePool;
    
    private int _currentLevel;

    public int CurrentLevel {  get { return _currentLevel; } }
    [HideInInspector] public PlayerMovement playerMovement;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;

        _currentLevel = PlayerPrefs.GetInt("LastCompletedLevel", 0) + 1;
    }

    private void Start()
    {
        _obstaclePool.Initialize();
        _obstacleSmokePool.Initialize();
        LoadingScreen.Instance.Activate(false);
    }
}
