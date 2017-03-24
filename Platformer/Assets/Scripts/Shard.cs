using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour {

	public float lowerAmount = 0.1f;

	private static bool isLowering = false;
	private static GameObject player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (isLowering) {
			Vector3 pos = player.transform.position;
			pos.y = transform.position.y - lowerAmount;
			transform.position = pos;
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
