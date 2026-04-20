using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
