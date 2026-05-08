using Google.Play.Review;
using System.Collections;
using UnityEngine;

public class IARManager : MonoBehaviour
{
    [SerializeField] private GameObject _rateButton;
    [SerializeField] private GameObject _rateMenu;

    private ReviewManager _reviewManager;
    private PlayReviewInfo _reviewInfo;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("LastCompletedLevel", 0) > 10 &&
            PlayerPrefs.GetInt("IsShowReview", 1) == 1)
        {
            _rateButton.SetActive(true);
        }
    }

    public void DisableRateMenu()
    {
        PlayerPrefs.SetInt("IsShowReview", 0);
        _rateMenu.SetActive(false);
        _rateButton.SetActive(false);
    }

    public void Review()
    {
        _reviewManager = new ReviewManager();
        StartCoroutine(RequestReview());
    }

    private IEnumerator RequestReview()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;

        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(requestFlowOperation.Error);
            yield break;
        }

        _reviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_reviewInfo);
        yield return launchFlowOperation;
        _reviewInfo = null;

        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            Debug.Log(launchFlowOperation.Error);
            yield break;
        }

        DisableRateMenu();
    }
}
