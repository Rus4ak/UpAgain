using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            gameObject.SetActive(false);
        }
    }
}
