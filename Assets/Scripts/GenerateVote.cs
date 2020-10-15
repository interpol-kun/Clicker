using UnityEngine;

public class GenerateVote : MonoBehaviour {

	public float lifetime;
	public float speed;
	// Use this for initialization
	void Start () {
		Invoke ("DeleteVote", lifetime);
	}
	void DeleteVote(){
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
        GetComponent<CanvasGroup>().alpha -= 0.005f;
		transform.Translate (Vector3.up * speed * Time.deltaTime);
	}
}
