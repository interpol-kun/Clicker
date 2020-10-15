using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

	public Click click;
	public Text itemInfo;
	public Text itemcost;
	public Text itemprofit;
	public Text quantity;
	public float cost;
	public int count = 0;
	public int clickPower;
	public string itemName;
	private float baseCost;
	public Color standard;
	public Color affordable;

    [SerializeField]
    private Image panel;

	void Start(){
		baseCost = cost;
		cost = Mathf.Round (baseCost * Mathf.Pow (1.15f, count));
        UpdateCost();
        panel = GetComponentInChildren<Transform>().Find("Panel").GetComponent<Image>();

    }
	void Update(){
		if (click.gold >= cost) {
			panel.color = affordable;
		} else {
			panel.color = standard;
		}
	}

	public void PurchaseUpgrade(){
		if (click.gold >= cost) {
			click.gold -= cost;
			count += 1;
			click.goldperclick += (float)clickPower;
			cost = Mathf.Round (baseCost * Mathf.Pow (1.15f, count));
            UpdateCost();
		}
	}
    private void UpdateCost()
    {
        itemcost.text = CurrencyConverter.GetCurrencyIntoString(cost);
        itemprofit.text = "+" + clickPower + "/КЛИК";
        quantity.text = count + "";
    }
}
