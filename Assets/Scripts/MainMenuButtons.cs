using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadGame(string gamelevel){
		SceneManager.LoadScene (gamelevel);
	}
	public void NewGame(string gamelevel){
		File.Delete (Application.persistentDataPath + "/playerInfo.dat");
		SceneManager.LoadScene (gamelevel);
	}
}
