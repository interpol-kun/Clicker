using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_animation_handler : MonoBehaviour {

	// Use this for initialization
	public void BackgroundSetBool(){
		gameObject.GetComponent<Animator>().SetBool ("playing", false);
	}
}
