using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour {
	public float movementSpeed = 10.0f;

	public float freq = 5.0f;
	public float amplitude = 60.0f;

	private float targetZ = 0.0f;

	public int score = 0;
	public float combo = 1.0f;
	public float hp = 1.0f;
	private int internalScore = 0;

	private GameObject collisionEffect;
	private GameObject pointEffect;

	private LevelManager levelManager;

	void Start () {
		collisionEffect = GameObject.Find("CollisionEffect");
		levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		pointEffect = GameObject.Find("PointEffect");
	}

	void Update () {
		if (!levelManager.inMenu) {
			float distanceX = transform.position.x;
			float distanceZ = transform.position.z;

			//score = (int) internalScore;

			targetZ = Mathf.Sin (distanceX / freq) * amplitude;
			Rigidbody rb = GetComponent<Rigidbody>();
			float movementZ = targetZ - transform.position.z;

			Vector3 vel = rb.velocity;
			vel.x = movementSpeed;
			vel.z = movementZ;
			rb.velocity = vel;

			Vector3 pos = transform.position;
			pos.z = targetZ;
			transform.position = pos;

			Quaternion rot = transform.rotation;
			rot.x = -(distanceZ / amplitude) * 0.5f;

			transform.rotation = rot;
			if (hp <= 0.0f) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Obstacles")) {

			GameObject g = Instantiate<GameObject>(collisionEffect);

			foreach(Collider c in g.GetComponents<Collider> ()) {
				c.enabled = false;
			}

			foreach(Collider c in g.GetComponentsInChildren<Collider> ()) {
				c.enabled = false;
			}
			combo = 1.0f;
			hp -= 0.1f;
//			Destroy (collision.gameObject);
			g.transform.position = collision.gameObject.transform.position;
		} else if (collision.gameObject.layer == LayerMask.NameToLayer ("Rupee")) {
			combo += 1.0f;
			GameObject g = Instantiate<GameObject>(pointEffect);
			g.transform.position = collision.gameObject.transform.position;
			Destroy (collision.gameObject);
			score += (int) (100 * combo);
		} else if (collision.gameObject.layer == LayerMask.NameToLayer ("EnvObstacles")) {
			GameObject g = Instantiate<GameObject>(collisionEffect);
			g.transform.position = collision.gameObject.transform.position;
			hp -= 0.1f;
		}
	}
}
