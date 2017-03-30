using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour {

	public float lowerAmount = 0.01f;

	private static bool isLowering = false;
	private static GameObject player;
	private static GameObject head;
	private float offset = 0.08f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		head = GameObject.Find ("Shard Target");
	}

	void Start () {
		Vector3 pos = transform.position;
		pos.x = player.transform.position.x + offset;
		pos.z = player.transform.position.z;
	}

	void Update () {
		if (isLowering) {
		//	Vector3 pos = player.transform.position;
		//	pos.y = transform.position.y - lowerAmount;
		//	transform.position = pos;
			Vector3 target = head.transform.position;
			target.x = player.transform.position.x + offset;
			target.z = player.transform.position.z;
			transform.position = Vector3.Lerp (transform.position, target, 0.01f);

			transform.RotateAround (transform.position, Vector3.up, 10.0f);
		}

	}

	public static void Activate () {
		isLowering = true;
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Protagonist") {
			isLowering = false;
			Destroy (gameObject);
			GameManager.instance.ShardHit ();
		}
	}
}
