using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoAds : MonoBehaviour
{
    [SerializeField] private Button _buyNoAdsButton;
    [SerializeField] private TMP_Text _buyNoAdsText;

    private void Start()
    {
        if (!AdsData.isShow)
        {
            InitializePurchase();
            return;
        }

        _buyNoAdsButton.onClick.AddListener(() => IAPManager.Instance.BuyProduct(IAPProductKey.RemoveAds));
        _buyNoAdsText.text = IAPManager.Instance.RemoveAdsPrice;
    }

    public void Buy()
    {
        InitializePurchase();
        AdsData.isShow = false;
    }

    private void InitializePurchase()
    {
        _buyNoAdsButton.interactable = false;
        _buyNoAdsText.text = "Activated";
        _buyNoAdsText.fontSize -= 10;
        _buyNoAdsText.color = new Color(.65f, .65f, .65f);
    }
}
