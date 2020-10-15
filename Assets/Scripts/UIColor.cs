using UnityEngine;
using UnityEngine.UI;

public class UIColor : MonoBehaviour {

	void Start () {
        GameControl.OnClicked += ChangeColor;
	}

    void ChangeColor()
    {
        gameObject.GetComponent<Image>().color = GameControl.CurrencyColor;
    }
}
