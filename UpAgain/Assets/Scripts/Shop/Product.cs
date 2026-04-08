using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private int _price = 100;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _countTMP;
    [SerializeField] private Button _buyButton;

    private AbilityType _type;
    private int _count;

    public Button BuyButton => _buyButton;

    public void Initialize(Sprite icon, string name, string description, AbilityType abilityType)
    {
        _icon.sprite = icon;
        _name.text = name;
        _description.text = description;
        _count = AbilitiesValues.values[abilityType];
        _countTMP.text = _count.ToString();

        _type = abilityType;
    }

    public void Buy()
    {
        if (Bank.Coins >= _price)
        {
            _count++;
            _countTMP.text = _count.ToString();
            
            Bank.Coins -= _price;
            AbilitiesValues.values[_type] = _count;

            PlayerPrefs.SetInt("Coins", Bank.Coins);
            PlayerPrefs.SetInt(_type.ToString(), _count);
        }
    }
}
