using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;
	[SerializeField] Vector2 deathKick = new Vector2(5f, 5f);

	bool isAlive = true;

	Rigidbody2D rb;
	Animator anim;
	CapsuleCollider2D col;
	BoxCollider2D feet;
	float initialGravity;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider2D> ();
		feet = GetComponent<BoxCollider2D> ();
		initialGravity = rb.gravityScale;
	}

	void FixedUpdate () {
		if (!isAlive) return;
		Run ();
		Climb ();
		Jump ();
		Flip ();
		Die();
	}

	void Run () {
		float xThrow = CrossPlatformInputManager.GetAxis ("Horizontal");
		rb.velocity = new Vector2 (xThrow * runSpeed, rb.velocity.y);

		bool hasXSpeed = Mathf.Abs (rb.velocity.x) > Mathf.Epsilon;
		anim.SetBool ("Running", hasXSpeed);
	}

	void Climb () {
		int climbingLayer = LayerMask.GetMask ("Climbing");
		bool isTouchingClimbable = feet.IsTouchingLayers (climbingLayer);
		if (!isTouchingClimbable) {
			anim.SetBool("Climbing", false);
			rb.gravityScale = initialGravity;
			return;
		}

		float yThrow = CrossPlatformInputManager.GetAxis ("Vertical");
		rb.velocity = new Vector2 (rb.velocity.x, yThrow * climbSpeed);
		rb.gravityScale = 0f;

		bool hasYSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
		anim.SetBool("Climbing", hasYSpeed);
	}

	void Jump () {
		int groundLayer = LayerMask.GetMask ("Ground");
		bool isTouchingGround = feet.IsTouchingLayers (groundLayer);
		if (!isTouchingGround) return;

		bool jumpPressed = CrossPlatformInputManager.GetButtonDown ("Jump");
		if (jumpPressed) {
			Vector2 jumpVelocity = new Vector2 (0f, jumpSpeed);
			rb.velocity += jumpVelocity;
		}
	}

	void Flip () {
		bool hasXSpeed = Mathf.Abs (rb.velocity.x) > Mathf.Epsilon;
		if (hasXSpeed) {
			transform.localScale = new Vector2 (Mathf.Sign (rb.velocity.x), 1f);
		}
	}

	void Die () {
		int enemyLayer = LayerMask.GetMask("Enemy", "Hazards");
		bool isTouchingEnemy = col.IsTouchingLayers(enemyLayer);
		if (isTouchingEnemy) {
			isAlive = false;
			anim.SetTrigger("Dying");
			rb.velocity = deathKick;
			FindObjectOfType<GameSession>().ProcessPlayerDeath();
		}
	}

}