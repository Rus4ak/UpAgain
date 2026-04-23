using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private RectTransform _content;

    [Header("Products")]
    [SerializeField] private Transform _productsMenu;
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private List<ProductSO> _products;

    [Header("Coins")]
    [SerializeField] private Transform _coinsMenu;
    [SerializeField] private GameObject _coinsPrefab;
    [SerializeField] private List<CoinsSO> _coins;

    private void Start()
    {
        UpdateBalance();
        InitializeProducts();
        InitializeCoins();

        LayoutRebuilder.ForceRebuildLayoutImmediate(_content);
    }

    public void UpdateBalance()
    {
        _coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

    private void InitializeProducts()
    {
        foreach (ProductSO product in _products)
        {
            Product productObj = Instantiate(_productPrefab, _productsMenu).GetComponent<Product>();
            productObj.Initialize(product.icon, product.title, product.description, product.abilityType);
            productObj.BuyButton.onClick.AddListener(UpdateBalance);
        }

        RectTransform productsMenuRectTransform = _productsMenu.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(productsMenuRectTransform);
    }

    private void InitializeCoins()
    {
        foreach (CoinsSO coin in _coins)
        {
            Coins coinsObj = Instantiate(_coinsPrefab, _coinsMenu).GetComponent<Coins>();
            coinsObj.Initialize(coin.id, coin.coinsCount);
        }

        RectTransform coinsMenuRectTransform = _coinsMenu.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(coinsMenuRectTransform);
    }

    public void BuyCoin(int count)
    {
        Bank.Coins += count;
        PlayerPrefs.SetInt("Coins", Bank.Coins);
        UpdateBalance();
    }
}
