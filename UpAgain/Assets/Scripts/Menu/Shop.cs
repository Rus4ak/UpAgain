using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        _coinsText.text = coins.ToString();
    }
}
