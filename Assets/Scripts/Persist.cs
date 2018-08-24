using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Persist : MonoBehaviour {

	int startingSceneIndex;

	static Persist singleton;

	void Awake () {
		if(!singleton) singleton = this;
		else Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
	}

	void Update () {
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneIndex != startingSceneIndex) Destroy(gameObject); 
	}
	
}
