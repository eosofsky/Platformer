using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHead : MonoBehaviour {

	private Transform lookPoint;

	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			/* Unparent the head from the body */
			gameObject.transform.parent = null;

			/* Throw the head */
			gameObject.AddComponent<CapsuleCollider> ();
			Rigidbody h = gameObject.AddComponent<Rigidbody> ();
			h.useGravity = false;
			Vector3 force = transform.forward * 500;
			h.AddForce (force);
			h.useGravity = true;

			/* Set to look at the body */
			lookPoint = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		transform.LookAt (lookPoint);
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag ("Floor")) {
			/* When it lands, remove the rigid body */
			Destroy(gameObject.GetComponent <Rigidbody> ());
		}
	}
}
