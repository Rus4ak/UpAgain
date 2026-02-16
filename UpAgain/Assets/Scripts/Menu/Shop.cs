using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);

        if (coins > 100000)
            _coinsText.text = "99999+";
        else
            _coinsText.text = coins.ToString();
    }
}
