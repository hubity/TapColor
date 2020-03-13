using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour
{

    public static AdMob instance;

	public int deaths;

	private BannerView bannerView;
	private InterstitialAd interstitial;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
#if UNITY_ANDROID
		string appId = "ca-app-pub-9726469706030794~4518368667";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-9785533804332092~9517523520";
#else
            string appId = "unexpected_platform";
#endif

		MobileAds.Initialize(initStatus => { });

		RequestBanner();
		RequestInterstitial();

	}

	public void RequestBanner()
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-9726469706030794/4250892896";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		bannerView.LoadAd(request);


		Invoke("DestroyBanner", 10f);
	}

	void DestroyBanner()
	{
		bannerView.Destroy();
	}

	private void RequestInterstitial()
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-9726469706030794/9797268007";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif



		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);

		this.interstitial.OnAdClosed += HandleOnAdClosed;

		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);

	}

	void HandleOnAdClosed(object sender, System.EventArgs args)
	{
		RequestInterstitial();
	}

	public void ShowPopUp()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
	}

}
