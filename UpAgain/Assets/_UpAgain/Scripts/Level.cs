using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _countToOpen;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private int _completedLevels;

    private void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Map.MapChanging += Initialize;
    }

    private void OnDisable()
    {
        Map.MapChanging -= Initialize;
    }

    private void Initialize()
    {
        if (_id == Map.CurrentMap)
        {
            DisableButton("Selected");
        }
        else
        {
            _completedLevels = PlayerPrefs.GetInt("LastCompletedLevel", 0) + 1;

            if (_completedLevels < _countToOpen)
            {
                DisableButton($"Level {_completedLevels}/{_countToOpen}");
            }
            else
            {
                EnableButton();
            }
        }
    }

    private void DisableButton(string text)
    {
        _button.interactable = false;
        _text.text = text;
        _text.color = new Color(.8f, .8f, .8f);
    }

    private void EnableButton()
    {
        _button.interactable = true;
        _text.text = "Select";
        _text.color = Color.white;
    }

    public void SelectMap()
    {
        Map.CurrentMap = _id;
    }
}
