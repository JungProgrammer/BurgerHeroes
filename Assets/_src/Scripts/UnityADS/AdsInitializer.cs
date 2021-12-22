using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] 
    private string _androidGameId;
    
    
    [SerializeField] 
    private string _iosGameId;
    
    
    [SerializeField] 
    private bool _testMode;


    private string _gameId;


    private void Awake()
    {
        InitializeAds();
    }


    public void InitializeAds()
    {
        _gameId = Application.platform == RuntimePlatform.IPhonePlayer ? _iosGameId : _androidGameId;
        
        Advertisement.Initialize(_gameId, _testMode);
    }
    
    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity ads initialization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads loading failed");
    }
}
