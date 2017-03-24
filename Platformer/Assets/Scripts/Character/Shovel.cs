using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour {

	private static bool canShovel = false; 
	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (canShovel && Input.GetMouseButtonDown (0)) {
			animator.SetTrigger ("Shovel");
		}
	}

	public static void Activate () {
		canShovel = true;
	}

	public static void Deactivate () {
		canShovel = false;
	}

}
