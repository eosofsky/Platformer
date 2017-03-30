using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchTrigger : MonoBehaviour {

	private Hatch hatch;
	private int numEnters;

	void Awake () {
		hatch = transform.parent.GetComponentInChildren<Hatch> ();
		numEnters = 0;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.CompareTag ("Wheelbarrower")) {
			//numEnters++;
			//if (numEnters == 1) {
				TargetWheelbarrow targetWheelbarrow = collider.GetComponentInChildren<TargetWheelbarrow> ();
				if (targetWheelbarrow) {
					Debug.Log ("Yes");
					targetWheelbarrow.SetTarget (true);
				}
				hatch.MoveUp ();
			//}
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.CompareTag ("Wheelbarrower")) {
			//numEnters--;
			//if (numEnters == 0) {
				TargetWheelbarrow targetWheelbarrow = collider.GetComponentInChildren<TargetWheelbarrow> ();
				if (targetWheelbarrow) {
					Debug.Log ("No");
					targetWheelbarrow.SetTarget (false);
				}
				hatch.MoveDown ();
			//}
		}
	}
}
