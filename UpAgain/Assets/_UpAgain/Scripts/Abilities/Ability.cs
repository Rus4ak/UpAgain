using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private int _reloadTime = 10;
    [SerializeField] private TMP_Text _reloadText;
    [SerializeField] private AbilityType _type;
    [SerializeField] private TMP_Text _countText;

    private int _count;
    private bool _isReload;
    private Color _defaultCountColor;
    private Image _buttonImage;
    private Image _countBadge;
    private AudioSource _effectSound;

    private void Start()
    {
        Initialize();
    }

    private void Use()
    {
        if (_isReload || _count <= 0)
            return;

        _isReload = true;

        StartCoroutine(Reload());
        UseAbility();

        _effectSound.Play();

        _count--;
        _countText.text = _count.ToString();
        AbilitiesValues.values[_type] = _count;
        PlayerPrefs.SetInt(_type.ToString(), _count);
    }

    IEnumerator Reload()
    {
        ChangeColor(new Color(.6f, .6f, .6f));
        _reloadText.gameObject.SetActive(true);

        int i = _reloadTime + 1;

        while (i > 0)
        {
            i--;
            _reloadText.text = i.ToString();

            yield return new WaitForSeconds(1);
        }

        ChangeColor(Color.white);

        _reloadText.gameObject.SetActive(false);
        _isReload = false;
    }

    private void ChangeColor(Color color)
    {
        _buttonImage.color = color;
        _countBadge.color = color;

        if (color == Color.white)
            _countText.color = _defaultCountColor;
        else
            _countBadge.color = color;
    }

    protected virtual void Initialize()
    {
        _count = AbilitiesValues.values[_type];
        _countText.text = _count.ToString();
        _defaultCountColor = _countText.color;
        _buttonImage = GetComponent<Image>();
        _countBadge = GetComponentInChildren<Image>();
        _effectSound = GetComponent<AudioSource>();

        GetComponent<Button>().onClick.AddListener(Use);
    }

    protected virtual void UseAbility() { }
}
