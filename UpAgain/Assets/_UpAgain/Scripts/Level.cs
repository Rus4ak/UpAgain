using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _countToOpen;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _level;

    private int _completedLevels;

    private void Start()
    {
        _level.text = $"{LevelsData.completedLevelsMap[_id] + 1} LEVEL";

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
            _completedLevels = LevelsData.completedAllLevels;

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
