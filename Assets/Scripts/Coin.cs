using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	[SerializeField] int pointsForCoinPickup = 100;
	[SerializeField] AudioClip coinPickupSFX;

	void OnTriggerEnter2D (Collider2D other) {
		FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
		AudioSource.PlayClipAtPoint (coinPickupSFX, Camera.main.transform.position);
		Destroy (gameObject);
	}

}