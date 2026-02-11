using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _finishMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            PlayerPrefs.SetInt("LastCompletedLevel", GameManager.Instance.CurrentLevel);
            _finishMenu.SetActive(true);
        }
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }
}
