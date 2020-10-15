using UnityEngine;
using System.Collections;

public class GoldPerSec : MonoBehaviour {

	public UnityEngine.UI.Text gpsDisplay;
	public Click click;
	public ItemManager[] items;

    void Start(){
		StartCoroutine (AutoTick ());
        UpdateGPSText();
	}

	public float GetGoldPerSec(){
		float tick = 0;
		foreach (ItemManager item in items) {
			tick += item.count * item.tickValue;
		}
		return tick;
	}

	public void AutoGoldPerSec(){
		click.gold += GetGoldPerSec () / 10;
	}

	IEnumerator AutoTick(){
		while (true) {
			AutoGoldPerSec ();
			yield return new WaitForSeconds (.1f);
		}
	}

    public void UpdateGPSText()
    {
        gpsDisplay.text = GetGoldPerSec() + "";
    }
}

