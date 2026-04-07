using UnityEngine;

[CreateAssetMenu(fileName = "NewShopProduct", menuName = "Shop/Product")]
public class ProductSO : ScriptableObject
{
    public Sprite icon;
    public string title;
    public string description;
    public AbilityType abilityType;
}
