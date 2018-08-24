using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	[SerializeField] float delay = 2f;
	[SerializeField][Range (0f, 1f)] float slowMotionTime = 0.5f;

	void OnTriggerEnter2D (Collider2D other) {
		StartCoroutine (LoadNextScene ());
	}

	IEnumerator LoadNextScene () {
		Time.timeScale = slowMotionTime;
		yield return new WaitForSecondsRealtime (delay);

		Time.timeScale = 1f;
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex + 1);
	}

}