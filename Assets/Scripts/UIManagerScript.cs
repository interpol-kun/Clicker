using UnityEngine;
using UnityEngine.EventSystems;

public class UIManagerScript : MonoBehaviour {

	public Animator contentPanel;
	public GameObject slave;
	public GameObject player;

	public void ToggleMenu() {
		string button_clicked = EventSystem.current.currentSelectedGameObject.name;
		bool isHidden = contentPanel.GetBool ("IsHidden");
		if (slave.activeSelf && player.activeSelf) {
			slave.SetActive (false);
		}
		if (button_clicked == "SlaveButton" && !slave.activeSelf) {
			if (!player.activeSelf) {
				slave.SetActive (true);
			} else if (player.activeSelf) {
				if (isHidden) {
					contentPanel.SetBool ("IsHidden", !isHidden);
					player.SetActive (false);
					slave.SetActive (true);
				} else {
					player.SetActive (false);
					slave.SetActive (true);
				}					
			}
		} else if (button_clicked == "SlaveButton" && slave.activeSelf) {
			contentPanel.SetBool ("IsHidden", !isHidden);
		}
		if (button_clicked == "PlayerButton" && !player.activeSelf) {
			if (!slave.activeSelf) {
				player.SetActive (true);
			} else if (slave.activeSelf) {
				if (isHidden) {
					contentPanel.SetBool ("IsHidden", !isHidden);
					slave.SetActive (false);
					player.SetActive (true);
				} else {
					slave.SetActive (false);
					player.SetActive (true);
				}		
			}

		} else if (button_clicked == "PlayerButton" && player.activeSelf) {
			contentPanel.SetBool ("IsHidden", !isHidden);
		}
	}		
}
