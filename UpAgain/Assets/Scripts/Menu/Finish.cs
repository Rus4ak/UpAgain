using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private Transform _chest;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _activateFinishMenuTime;
    [SerializeField] private TMP_Text _rewardText;

    private Animator _chestAnimator;
    private CameraMovement _mainCamera;
    private int _currentLevel;

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

            PlayerPrefs.SetInt("LastCompletedLevel", _currentLevel);

            Vector3 cameraLookAt = _chest.position;
            cameraLookAt.y += 1;

            _mainCamera.SetTarget(_chest, _cameraOffset, cameraLookAt);
            _chestAnimator.SetBool("IsOpen", true);
            
            Invoke(nameof(ActiveFinishMenu), _activateFinishMenuTime);
        }
    }

    private void ActiveFinishMenu()
    {
        int rewardCoins = Random.Range(_currentLevel, _currentLevel + 100);
        
        _rewardText.text = $"+{rewardCoins}";
        Canvas.ForceUpdateCanvases();
        
        Bank.Coins += rewardCoins;
        PlayerPrefs.SetInt("Coins", Bank.Coins);

        _finishMenu.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
