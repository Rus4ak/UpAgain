using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;
    [SerializeField] private Transform _chest;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _activateFinishMenuTime;

    private Animator _chestAnimator;
    private CameraMovement _mainCamera;

    private void Start()
    {
        _chestAnimator = _chest.GetComponent<Animator>();
        _mainCamera = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("LastCompletedLevel", GameManager.Instance.CurrentLevel);

            Vector3 cameraLookAt = _chest.position;
            cameraLookAt.y += 1;

            _mainCamera.SetTarget(_chest, _cameraOffset, cameraLookAt);
            _chestAnimator.SetBool("IsOpen", true);
            
            Invoke(nameof(ActiveFinishMenu), _activateFinishMenuTime);
        }
    }

    private void ActiveFinishMenu()
    {
        _finishMenu.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
