using UnityEngine;
using System.Collections;

public class GenerateIdleVotes : MonoBehaviour {

	public float lifetime;
	// Use this for initialization
	void Start () {
		Invoke ("DeleteIdleVote", lifetime);
	}
	void DeleteIdleVote(){
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
		this.GetComponent<CanvasGroup> ().alpha -= 0.003f;
	}
}
