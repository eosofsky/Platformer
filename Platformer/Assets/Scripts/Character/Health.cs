using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

	private static int health;

	void Awake () {
		health = 3;
	}

	public static void TakeHit () {
		health--;
		//Debug.Log ("health: " + health);
		if (health == 0) {
			// Lose
			SceneManager.LoadScene (4);
		}
	}
}
