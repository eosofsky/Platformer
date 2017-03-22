using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour {

	private Animator animator;
	private static bool canDraw = true;
	private bool isTurning = false;
	private Transform target;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	void LateUpdate () {
		if (isTurning) {
			Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
			Quaternion currentRot = transform.localRotation;
			targetRot.x = currentRot.x;
			targetRot.z = currentRot.z;
			transform.localRotation = Quaternion.Slerp (currentRot, targetRot, 0.1f);
			if (AlmostEqual(transform.localRotation.y, targetRot.y)) {
				isTurning = false;
				animator.SetTrigger ("Draw");
			}
		}
	}

	public static void SetDrawable (bool drawable) {
		canDraw = drawable;
	}

	void OnTriggerEnter (Collider collider) {
		if (canDraw && collider.name == "Lever Operator" /*&& Input.GetKeyDown (KeyCode.L)*/) {
			target = collider.transform;
			isTurning = true;
		}
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.07f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}
