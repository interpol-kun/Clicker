using UnityEngine;

public class CurrencyConverter : ScriptableObject{
	
	public static string GetCurrencyIntoString(float valueToConvert){
		string converted;

		if (valueToConvert >= 1000000.0) {
			converted = (valueToConvert / 1000000f).ToString ("f3") + " M";
		} else if (valueToConvert >= 1000.0) {
			converted = (valueToConvert / 1000f).ToString ("f3") + " K";
		} else {
			converted = "" + Mathf.Round(valueToConvert);
		}
		return converted;
	}
}
