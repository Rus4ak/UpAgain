using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCountText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _buyButton;

    public string CoinsID {  get; private set; }

    public void Initialize(string coinsId, int coinsCount)
    {
        CoinsID = coinsId;
        _coinsCountText.text = coinsCount.ToString();

        IAPManager manager = IAPManager.Instance;
        string price = "";

        switch (CoinsID)
        {
            case "coin100":
                _buyButton.onClick.AddListener(() => manager.BuyProduct(IAPProductKey.Coin100));
                price = manager.Coin100Price;
                break;
            case "coin300":
                _buyButton.onClick.AddListener(() => manager.BuyProduct(IAPProductKey.Coin300));
                price = manager.Coin300Price;
                break;
            case "coin500":
                _buyButton.onClick.AddListener(() => manager.BuyProduct(IAPProductKey.Coin500));
                price = manager.Coin500Price;
                break;
            case "coin1000":
                _buyButton.onClick.AddListener(() => manager.BuyProduct(IAPProductKey.Coin1000));
                price = manager.Coin1000Price;
                break;
        }

        _priceText.text = price;
    }
}
