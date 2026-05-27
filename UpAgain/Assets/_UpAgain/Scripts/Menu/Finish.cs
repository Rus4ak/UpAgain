using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private Transform _chest;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _activateFinishMenuTime;
    [SerializeField] private RectTransform _rewardLayout;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private AudioSource _rewardSound;

    private Animator _chestAnimator;
    private CameraMovement _mainCamera;
    private int _currentLevel;
    private string _loadScene;

    private void Start()
    {
        _chestAnimator = _chest.GetComponent<Animator>();
        _mainCamera = Camera.main.GetComponent<CameraMovement>();
        _currentLevel = GameManager.Instance.CurrentLevel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.playerMovement.SetMove(false);

            LevelsData.completedLevelsMap[Map.CurrentMap] = _currentLevel;

            if (Map.CurrentMap == 0)
                PlayerPrefs.SetInt("LastCompletedLevel", _currentLevel);
            else if (Map.CurrentMap == 1)
                PlayerPrefs.SetInt("LastCompletedLevelMap2", _currentLevel);

            Vector3 cameraLookAt = _chest.position;
            cameraLookAt.y += 1;

            _mainCamera.SetTarget(_chest, _cameraOffset, cameraLookAt);
            _chestAnimator.SetBool("IsOpen", true);
            
            Invoke(nameof(ActiveFinishMenu), _activateFinishMenuTime);
        }
    }

    private void ActiveFinishMenu()
    {
        int rewardCoins = Random.Range(_currentLevel, _currentLevel + 50);
        
        _rewardText.text = $"+{rewardCoins}";
        
        Bank.Coins += rewardCoins;
        PlayerPrefs.SetInt("Coins", Bank.Coins);

        _finishMenu.SetActive(true);
        _rewardSound.Play();

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rewardLayout);
    }

    public void ShowAd(string loadSceneName)
    {
        _loadScene = loadSceneName;

        InterstitialAdShow.Instance.ProcessAd += LoadScene;

        InterstitialAdShow.Instance.Show();
    }

    private void OnDisable()
    {
        InterstitialAdShow.Instance.ProcessAd -= LoadScene;
    }

    private void LoadScene()
    {
        LoadingScreen.Instance.Activate(true);
        SceneManager.LoadSceneAsync(_loadScene);
        Destroy(Music.Instance.gameObject);
    }
}
