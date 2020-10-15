using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Achievment {
	public string title;
	public string desc;
	public Sprite image;
	public bool not_reached;
}

public class AchievmentManager : MonoBehaviour {

	public Click click;
	public GoldPerSec GPC;
	public GameObject Achievment;
	private GameObject _achievment;
	public List<Achievment> achievment_list;
	public GameObject background;
	private float timer = 5.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (click.Gold_overall > 1000 && achievment_list[0].not_reached && !_achievment) {
			SpawnAchievment (0, achievment_list[0].title, achievment_list[0].desc, out achievment_list[0].not_reached);
		}
		if (click.Gold_overall > 10000 && achievment_list[1].not_reached && !_achievment ){
			SpawnAchievment (1,  achievment_list[1].title, achievment_list[1].desc, out achievment_list[1].not_reached);
		}
		if (click.click_count > 1000 && achievment_list [2].not_reached && !_achievment) {
			SpawnAchievment (2, achievment_list [2].title, achievment_list [2].desc, out achievment_list [2].not_reached);
		}
		if (click.Gold_overall > 1000000 && achievment_list [3].not_reached && !_achievment) {
			SpawnAchievment (3, achievment_list [3].title, achievment_list [3].desc, out achievment_list [3].not_reached);
		}
		if (GPC.items[7].count >= 100 && achievment_list [4].not_reached && !_achievment) {
			SpawnAchievment (4, achievment_list [4].title, achievment_list [4].desc, out achievment_list [4].not_reached);
		}
		if (GPC.items[6].count >= 100 && achievment_list [5].not_reached && !_achievment) {
			SpawnAchievment (5, achievment_list [5].title, achievment_list [5].desc, out achievment_list [5].not_reached);
		}
		if (GPC.GetGoldPerSec() >= 1000000.00f && achievment_list [6].not_reached && !_achievment) {
			SpawnAchievment (6, achievment_list [6].title, achievment_list [6].desc, out achievment_list [6].not_reached);
		}
		if (_achievment) {
			timer -= Time.deltaTime;
			if (timer <= 4.0f) {
				_achievment.GetComponent<CanvasGroup> ().alpha -= 0.005f;
			}
			if (timer <= 0.0f) {
				Destroy (_achievment);
			}
		}
	
	}
	void SpawnAchievment(int n, string title, string desc, out bool a){
		timer = 12.00f;
		_achievment = Instantiate (Achievment) as GameObject;
		_achievment.transform.SetParent (this.transform);
		_achievment.transform.position = this.transform.position;
	    _achievment.transform.rotation = this.transform.rotation;
		_achievment.transform.localScale = new Vector3 (1, 1, 1);
		_achievment.GetComponentInChildren<Transform>().Find("AchImage").GetComponent<Image>().overrideSprite = achievment_list[n].image;
		_achievment.GetComponentInChildren<Transform> ().Find ("RawImage").GetComponentInChildren<Transform> ().Find ("Desc").GetComponent<Text> ().text = desc;
		_achievment.GetComponentInChildren<Transform> ().Find ("RawImage").GetComponentInChildren<Transform> ().Find ("Name").GetComponent<Text> ().text = title;
		a = false;
	}
}
