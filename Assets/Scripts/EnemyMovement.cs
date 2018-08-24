using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;

	Rigidbody2D rb;
	BoxCollider2D hitbox;	

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		bool isFacingRight = transform.localScale.x > Mathf.Epsilon;
		if (isFacingRight) rb.velocity = Vector2.right * moveSpeed;
		else rb.velocity = Vector2.left * moveSpeed;
	}

	void OnTriggerExit2D(Collider2D col) {
		transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
	}

}
