using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsText;

    [Header("Products")]
    [SerializeField] private Transform _productsMenu;
    [SerializeField] private GameObject _productPrefab;
    [SerializeField] private List<ProductSO> _products;

    private void Start()
    {
        UpdateBalance();

        foreach (ProductSO product in _products)
        {
            Product productObj = Instantiate(_productPrefab, _productsMenu).GetComponent<Product>();
            productObj.Initialize(product.icon, product.title, product.description, product.abilityType);
            productObj.BuyButton.onClick.AddListener(UpdateBalance);
        }
    }

    private void UpdateBalance()
    {
        _coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
}
