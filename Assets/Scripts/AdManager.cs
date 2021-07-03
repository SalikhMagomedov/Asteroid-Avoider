using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;
    
    public static AdManager Instance;

    private GameOverHandler _gameOverHandler;
#if UNITY_ANDROID
    private const string GameId = "4201413";
#elif UNITY_IOS
    private const string GameId = "4201412";
#endif

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            Advertisement.AddListener(this);
            Advertisement.Initialize(GameId, testMode);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        _gameOverHandler = gameOverHandler;
        Advertisement.Show("Rewarded_Android");
    }

    public void OnUnityAdsReady(string placementId)
    {    
        Debug.Log("Unity Ads Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                _gameOverHandler.ContinueGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
        }
    }
}