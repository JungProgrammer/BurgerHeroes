using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    [SerializeField] 
    private BannerPosition _bannerPosition;


    [SerializeField] 
    private string _androidAdUnitId = "Banner_Android";
    
    
    [SerializeField] 
    private string _iOSAdUnitId = "Banner_iOS";


    private string _adUnitId;


    private void Awake()
    {
        _adUnitId = Application.platform == RuntimePlatform.IPhonePlayer ? _iOSAdUnitId : _androidAdUnitId;
    }


    private void Start()
    {
        Advertisement.Banner.SetPosition(_bannerPosition);

        StartCoroutine(LoadBannerAfterStartDelay());
    }


    private IEnumerator LoadBannerAfterStartDelay()
    {
        yield return new WaitForSeconds(1f);
        
        LoadBanner();
    }


    public void LoadBanner()
    {
        BannerLoadOptions bannerLoadOptions = new BannerLoadOptions()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        
        
        Advertisement.Banner.Load(_adUnitId, bannerLoadOptions);
    }


    private void OnBannerLoaded()
    {
        ShowBannerAd();
    }


    private void OnBannerError(string message)
    {
        Debug.Log($"Banner loaded Error: {message}");
    }
    
    
    public void ShowBannerAd()
    {
        BannerOptions bannerOptions = new BannerOptions()
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShow
        };
        
        
        Advertisement.Banner.Show(_adUnitId, bannerOptions);
    }


    private void OnBannerClicked() { }


    private void OnBannerHidden() { }


    private void OnBannerShow() { }


    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
