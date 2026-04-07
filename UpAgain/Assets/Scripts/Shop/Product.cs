using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _count;

    public void Initialize(Sprite icon, string name, string description, AbilityType abilityType)
    {
        _icon.sprite = icon;
        _name.text = name;
        _description.text = description;
        _count.text = $"x{Abilities.values[abilityType]}";
    }
}
