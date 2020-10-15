using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

	public Text itemInfo;
	public Text itemcost;
	public Text itemprofit;
	public Text quantity;
	public Click Click;
	public float cost;
	public int tickValue;
	public int count;
	public string itemName;
	private float baseCost;
	public Color standard;
	public Color affordable;

    [SerializeField]
    private GoldPerSec gps;
    [SerializeField]
    private Image panel;

	void Start(){
		baseCost = cost;
		cost = Mathf.Round (baseCost * Mathf.Pow (1.15f, count));
        UpdateCost();
        panel = GetComponentInChildren<Transform>().Find("Panel").GetComponent<Image>();
    }

    private void OnEnable()
    {
        gps = Click.GoldPerSec;
    }

    void Update(){
		if (Click.gold >= cost) {
			panel.color = affordable;
		} else {
			panel.color = standard;
		}
	}

	public void PurchaseItem(){
		if (Click.gold >= cost) {
			Click.gold -= cost;
			count++;
			cost = Mathf.Round (baseCost * Mathf.Pow (1.15f, count));
            UpdateCost();
		}
    }
    private void UpdateCost()
    {
        itemcost.text = CurrencyConverter.GetCurrencyIntoString(cost);
        itemprofit.text = "ПОСТОВ: " + tickValue + "/С";
        quantity.text = count + "";
        gps.UpdateGPSText();
    }
}