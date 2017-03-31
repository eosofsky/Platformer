using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag ("Player")) {
			Debug.Log ("lose yes");
		}
	}
}
