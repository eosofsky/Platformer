using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

	private Animator animator;
	private static bool canDraw = false;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public static void SetDrawable (bool drawable) {
		canDraw = drawable;
	}

	void OnTriggerEnter (Collider collider) {
		if (canDraw && collider.name == "Lever Operator") {
			animator.SetTrigger ("Draw");
		}
	}
}
