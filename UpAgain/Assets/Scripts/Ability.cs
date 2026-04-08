using TMPro;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private AbilityType _type;
    [SerializeField] private TMP_Text _count;

    private void Start()
    {
        _count.text = AbilitiesValues.values[_type].ToString();
    }
}
