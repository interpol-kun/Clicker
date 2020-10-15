using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameControl : MonoBehaviour {
	public Click Click;
	public GoldPerSec GPS;
	public AchievmentManager Achievmnets;
	public UpgradeControl Upgrades;
	public GameObject IdleVotes;

	private TimeSpan t = new TimeSpan();
    
    public static Color CurrencyColor;

    private bool firstLaunch = true;

    [Space(2)]
    [Header("Currency variables")]
    [SerializeField]
    private Color bitcoin;
    [SerializeField]
    private Color ethereum;
    [SerializeField]
    private GameObject currencyPickPanel;
    [SerializeField]
    private GameObject bitcoinButton;
    [SerializeField]
    private GameObject ethereumButton;
    [SerializeField]
    private Material bitcoinMaterial;
    [SerializeField]
    private Material ethereumMaterial;

    public delegate void ColorChange();
    public static event ColorChange OnClicked;

    enum CurrencyPick
    {
        Bitcoin,
        Ethereum
    };

    [SerializeField]
    private int currencyPick;

	void OnApplicationQuit(){
		Save ();
	}

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
		Load ();
		IdleVote ();
    }

	void Start () {
		InvokeRepeating ("Save", 5, 5);
        if (firstLaunch)
        {
            currencyPickPanel.SetActive(true);
            bitcoinButton.GetComponent<Image>().color = bitcoin;
            ethereumButton.GetComponent<Image>().color = ethereum;
        }
        else
        {
            if(currencyPick == (int)CurrencyPick.Bitcoin)
            {
                ApplyBtc();
            }else if(currencyPick == (int)CurrencyPick.Ethereum)
            {
                ApplyEth();
            }
        }
        LogIn();
    }

    public void PickEth()
    {
        currencyPick = (int)CurrencyPick.Ethereum;
        ApplyEth();
        ConfirmPick();
    }

    public void PickBtc()
    {
        currencyPick = (int)CurrencyPick.Bitcoin;
        ApplyBtc();
        ConfirmPick();
    }

    private void ConfirmPick()
    {
        Destroy(currencyPickPanel);
        firstLaunch = false;
        if (OnClicked != null)
            OnClicked();
    }

    private void ApplyBtc()
    {
        CurrencyColor = bitcoin;
        Click.BackgroundImage.material = bitcoinMaterial;
    }

    private void ApplyEth()
    {
        CurrencyColor = ethereum;
        Click.BackgroundImage.material = ethereumMaterial;
    }

	
	private void IdleVote(){
        if (!firstLaunch)
        {
            GameObject v = Instantiate(IdleVotes);
            v.transform.SetParent(this.transform);
            v.transform.position = this.transform.position;
            v.GetComponentInChildren<Transform>().Find("Votes").GetComponent<Text>().text = "" + CurrencyConverter.GetCurrencyIntoString((GPS.GetGoldPerSec() * (float)t.TotalSeconds) / 10.00f);
        }		
	}

    private void ConnectToGooglePlay()
    {
        Social.localUser.Authenticate((bool success) =>
            {
               
        });
    }


    private void LogIn()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        ConnectToGooglePlay();
    }


    public void ShowAchievment()
    {
         Social.ShowAchievementsUI();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.gold = Click.gold;
        data.goldperclick = Click.goldperclick;
        data.gold_overall = Click.Gold_overall;
        data.click_count = Click.click_count;
        for (int i = 0; i < GPS.items.Length; i++)
        {
            data.items.Add(GPS.items[i].count);
        }

        for (int i = 0; i < Upgrades.upgrades.Length; i++)
        {
            data.upgrades.Add(Upgrades.upgrades[i].count);
        }

        for (int i = 0; i < Achievmnets.achievment_list.Count; i++)
        {
            data.achievments.Add(Achievmnets.achievment_list[i].not_reached);
        }
        data.lastPlayDate = DateTime.Now;

        data.firstLaunch = firstLaunch;
        data.currencyPick = currencyPick;

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            t = DateTime.Now - data.lastPlayDate;

            Click.goldperclick = data.goldperclick;
            Click.click_count = data.click_count;

            firstLaunch = data.firstLaunch;
            currencyPick = data.currencyPick;

            for (int i = 0; i < data.items.Count; i++)
            {
                GPS.items[i].count = data.items[i];
            }
            for (int i = 0; i < data.upgrades.Count; i++)
            {
                Upgrades.upgrades[i].count = data.upgrades[i];
            }
            for (int i = 0; i < data.achievments.Count; i++)
            {
                Achievmnets.achievment_list[i].not_reached = data.achievments[i];
            }
            Click.gold = data.gold + (GPS.GetGoldPerSec() * (float)t.TotalSeconds) / 10.00f;
            Click.Gold_overall = data.gold_overall + (GPS.GetGoldPerSec() * (float)t.TotalSeconds) / 10.00f;
        }
    }
}

[Serializable]
class PlayerData{
	public float gold_overall;
	public float gold;
	public float goldperclick;
	public int click_count;
	public List<int> items = new List<int>();
	public List<int> upgrades = new List<int> ();
	public List<bool> achievments = new List<bool>();
	public DateTime lastPlayDate;
    public bool firstLaunch;
    public int currencyPick;
}
