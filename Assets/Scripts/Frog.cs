using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Frog : MonoBehaviour {

	[Range(1, 10)]
	public float jumpVelocity;
	[Range(1, 10)] 
	public float lrVelocity;

	private int waterCount;
	public Text waterCountText;

	public Text winText;

	Rigidbody2D rb;


	private HealthBar healthBar;

	void Awake() {
		healthBar = GetComponent<HealthBar> ();

	}

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
		//healthbar = otherGameObject.GetComponent<HealthBar> ();
		waterCount = 0;
		SetCountText ();
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			rb.velocity = Vector2.up * jumpVelocity;
		}

		if (Input.GetKey (KeyCode.A)) {
			rb.AddForce (Vector2.left * lrVelocity);
			transform.rotation = new Quaternion (0, 180, 0, 0);
		}
		if (Input.GetKey (KeyCode.D)) {
			rb.AddForce (Vector2.right * lrVelocity);
			transform.rotation = new Quaternion (0, 0, 0, 0);
		}

		healthBar.TakeDamage (0.05f);
	}

	//this function will be called each time we touch a trigger collider
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("WaterDrop")) {
			other.gameObject.SetActive (false);
			waterCount = waterCount + 1;
			SetCountText ();

			healthBar.HealDamage (10);
		} else if (other.gameObject.CompareTag ("Bomb")) {
			other.gameObject.SetActive (false);

			healthBar.TakeDamage (10);

		} else if (other.gameObject.CompareTag ("Fountain")) {
			other.gameObject.SetActive (false);
			winText.text = "Yeah! You save the world!";
		}
	}

	void SetCountText() {
		waterCountText.text = "Water Amount: " + waterCount.ToString ();
	}
}
