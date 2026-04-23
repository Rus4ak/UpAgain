using UnityEngine;

[CreateAssetMenu(fileName = "NewShopCoinsProduct", menuName = "Shop/Coins")]
public class CoinsSO : ScriptableObject
{
    public string id;
    public int coinsCount;
}
