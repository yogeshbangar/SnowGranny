using System.Collections;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdManager
{
    private static AdManager sInstance = new AdManager();
    public static AdManager Instance
	{
		get{return sInstance;}
	}
    InterstitialAd mInterstitial;
    
    public void Init()
	{

        MobileAds.Initialize(initStatus => { });
        if (this.mInterstitial == null) {
            this.mInterstitial = new InterstitialAd ("ca-app-pub-3940256099942544/1033173712");//Sample test ID for Admob
            this.mInterstitial.OnAdClosed += HandleOnAdClosed;
            Debug.Log ("innnnnnnnnnnn       Init  mInterstitial ");
        }
        LoadInterstitial();
    }
    

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleOnAdClosed111111111111");
         LoadInterstitial();
    }
    public void LoadInterstitial()
	{
        if (this.mInterstitial != null && !this.mInterstitial.IsLoaded())
        {
            //Debug.Log ("innnnnnnnnnnn       LoadInterstitial ");
            this.mInterstitial.LoadAd(new AdRequest.Builder().Build());
        }
    }
	public void ShowInterstitial()
	{
        if (this.mInterstitial != null && this.mInterstitial.IsLoaded())
        {
            this.mInterstitial.Show();
        }
        
    }
}
