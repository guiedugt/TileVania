using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	[SerializeField] int playerLives = 3;
	[SerializeField] int score = 0;

	[SerializeField] Text livesText;
	[SerializeField] Text scoreText;

	static GameSession singleton;

	void Awake () {
		if (!singleton) singleton = this;
		else Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		livesText.text = playerLives.ToString ();
		scoreText.text = score.ToString ();
	}

	public void AddToScore (int pointsToAdd) {
		score += pointsToAdd;
		scoreText.text = score.ToString ();
	}

	public void ProcessPlayerDeath () {
		if (playerLives > 1) TakeLife ();
		else ResetGameSession ();
	}

	void TakeLife () {
		playerLives--;
		livesText.text = playerLives.ToString ();
		var currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
	}

	void ResetGameSession () {
		SceneManager.LoadScene (0);
		Destroy (gameObject);
	}

}