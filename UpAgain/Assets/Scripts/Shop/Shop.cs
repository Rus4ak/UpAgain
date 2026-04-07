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
        int coins = PlayerPrefs.GetInt("Coins", 0);
        _coinsText.text = coins.ToString();

        foreach (ProductSO product in _products)
        {
            GameObject productObj = Instantiate(_productPrefab, _productsMenu);
            productObj.GetComponent<Product>().Initialize(product.icon, product.title, product.description, product.abilityType);
        }
    }
}
