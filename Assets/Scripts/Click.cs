using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour {

	public Text gpc;
	public Text goldDisplay;
	public GameObject vote;
	private float gold_overall = 0.00f;
	public float gold = 0.00f;
	public float goldperclick = 1.00f;
	public int click_count = 0;

    public GoldPerSec GoldPerSec;

    [SerializeField]
    private Image backgroundImage;

	void Update(){
		goldDisplay.text = CurrencyConverter.GetCurrencyIntoString (gold);
		gpc.text = CurrencyConverter.GetCurrencyIntoString(goldperclick);
	}
	public void Clicked(){
		AddGold(goldperclick);
        if(Input.touchCount > 0)
        {
            float randomX = Input.GetTouch(0).position.x + Random.Range(-20, 20);
            float posY = Input.GetTouch(0).position.y;
            InstantiateVote(randomX, posY);
        }
		click_count++;
	}

	void InstantiateVote(float randomX, float posY){

		Vector3 pos = new Vector3 (randomX, posY, 0);
		GameObject t = Instantiate (vote);
		t.transform.SetParent (this.transform);
		t.transform.position = pos;
        Debug.Log("Click");
		t.transform.localScale = gameObject.transform.localScale;
		t.GetComponentInChildren<Transform> ().Find ("gpc").GetComponent<Text>().text = "+" + CurrencyConverter.GetCurrencyIntoString(goldperclick);
	}
	
	public void AddGold(float amount){
		gold += amount;
		gold_overall += amount;
	}
	public float Gold_overall{
		get
		{
			return gold_overall;
		}
		set 
		{
			gold_overall = value;
		}
	}

    public Image BackgroundImage
    {
        get
        {
            return backgroundImage;
        }
        set
        {
            backgroundImage = value;
        }
    }
}
