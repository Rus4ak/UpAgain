using UnityEngine;

[CreateAssetMenu(fileName = "NewShopCoinsProduct", menuName = "Shop/Coins")]
public class CoinsSO : ScriptableObject
{
    public int coinsCount;
    public float price;
}
