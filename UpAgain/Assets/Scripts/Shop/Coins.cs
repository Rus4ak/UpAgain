using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCountText;
    [SerializeField] private TMP_Text _priceText;

    public void Initialize(int coinsCount, float price)
    {
        _coinsCountText.text = coinsCount.ToString();
        _priceText.text = price.ToString() + "$";
    }
}
