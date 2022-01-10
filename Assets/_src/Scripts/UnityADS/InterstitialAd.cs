using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitialAd Instance;


    [SerializeField]
    private string _androidAdUnitId = "Interstitial_Android";


    [SerializeField]
    private string _iOSAdUnitId = "Interstitial_iOS";


    private string _adUnitId;


    private void Awake()
    {
        Instance = this;
        _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ? _iOSAdUnitId : _androidAdUnitId;
    }


    private void Start()
    {
        LoadAd();
    }


    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }


    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }


    public void OnUnityAdsAdLoaded(string placementId)
    {

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad unity: {_adUnitId} - {error.ToString()}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {

    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }
}
