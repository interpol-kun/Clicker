using UnityEngine;

public class TimurRandom : MonoBehaviour {	

	private float lifetime = 10.00f;

	void Start(){
		Invoke ("Die", lifetime);
	}

	void Update(){

	}

	public void Clicked(){
		GameObject.FindObjectOfType<TimurSpawner>().DestroyTim (true);	
	}

	private void Die(){
		GameObject.FindObjectOfType<TimurSpawner> ().DestroyTim (false);	
	}
				
}
