using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag ("Player")) {
			// Lose
			SceneManager.LoadScene (4);
		}
	}
}
