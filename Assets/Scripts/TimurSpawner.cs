using UnityEngine;
public class TimurSpawner : MonoBehaviour {

	private GameObject _timur;

	public GameObject Timur;
	public Click Click;
	public float timer = 600.0f;

	public bool ReadyToSpawn = true;

    [SerializeField]
    private int power;
	
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0.0f && ReadyToSpawn) {
			SpawnObject ();
			timer = 600.0f;
		}
	}

	public void SpawnObject(){
		Vector3 screenPosition = new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), 0);
		_timur = Instantiate (Timur) as GameObject;
		_timur.transform.SetParent (this.transform);
		_timur.transform.position = screenPosition;
		_timur.transform.rotation = this.transform.rotation;
		_timur.transform.localScale = this.transform.localScale;
		ReadyToSpawn = false;
	}
	public void DestroyTim(bool clicked){
		if (clicked) {
			Click.AddGold (Click.goldperclick*power);
		}
		Destroy (_timur);
		ReadyToSpawn = true;
	}
}

