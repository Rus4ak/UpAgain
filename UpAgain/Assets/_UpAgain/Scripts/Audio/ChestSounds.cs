using UnityEngine;

public class ChestSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _spin;
    [SerializeField] private AudioSource _bounce;
    [SerializeField] private AudioSource _opening;
    [SerializeField] private AudioSource _coins;

    public void SpinPlay()
    {
        _spin.Play();
    }

    public void BouncePlay()
    {
        _bounce.Play();
    }

    public void OpenPlay()
    {
        _opening.Play();
    }

    public void CoinsPlay()
    {
        _coins.Play();
    }
}
