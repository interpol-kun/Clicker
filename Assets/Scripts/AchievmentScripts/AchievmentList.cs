using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievmentList : MonoBehaviour {

	public GameObject Achievment;
	public AchievmentManager Achiemvents;
	public Transform ContentPanel;
	private List<GameObject> AchievmentInstances = new List<GameObject>();
	// Use this for initialization
	void Start () {
		PopulateList ();
		InvokeRepeating ("UpdateList", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PopulateList(){
		//create list of all achievments
		foreach(var achievment in Achiemvents.achievment_list){
			GameObject newAchievment = Instantiate (Achievment) as GameObject;
			newAchievment.GetComponentInChildren<Transform>().Find("AchImage").GetComponent<Image>().overrideSprite = achievment.image;
			newAchievment.GetComponentInChildren<Transform> ().Find ("RawImage").GetComponentInChildren<Transform> ().Find ("Desc").GetComponent<Text> ().text = achievment.desc;
			newAchievment.GetComponentInChildren<Transform> ().Find ("RawImage").GetComponentInChildren<Transform> ().Find ("Name").GetComponent<Text> ().text = achievment.title;
			newAchievment.transform.SetParent (ContentPanel);
			newAchievment.transform.localScale = new Vector3 (1, 1, 1);
			if (achievment.not_reached != true) {
				newAchievment.GetComponent<Image> ().color = new Color32 (179, 250, 210, 255);
			} else {
				newAchievment.GetComponent<Image> ().color = new Color32 (255, 165, 165, 255);
			}
			AchievmentInstances.Add (newAchievment);
		}
	}
	void UpdateList(){
		for (int i = 0; i < AchievmentInstances.Count; i++) {
			if (Achiemvents.achievment_list[i].not_reached != true) {
				AchievmentInstances[i].GetComponent<Image> ().color = new Color32 (179, 250, 210, 255);
			} else {
				AchievmentInstances[i].GetComponent<Image> ().color = new Color32 (255, 165, 165, 255);
			}
		}
	}
}
