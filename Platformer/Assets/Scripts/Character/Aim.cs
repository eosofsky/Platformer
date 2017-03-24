using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {

	private Ray point;
	private RaycastHit pointHit;

	public static Transform Target;
	private static bool AimEnabled = false;

	public GameObject targetIndicator;

	public static void Activate () {
		AimEnabled = true;
	}

	public static void Deactivate () {
		AimEnabled = false;
	}

	void Update () {
		if (AimEnabled) {
			point = Camera.main.ScreenPointToRay (Input.mousePosition);

			int layerMask = (1 << 8);
			if (Physics.Raycast (point, out pointHit, 10000, layerMask)) {
				targetIndicator.transform.position = pointHit.transform.position;
				targetIndicator.SetActive (true);
				Target = pointHit.transform;
			} else {
				targetIndicator.SetActive (false);
				Target = null;
			}
		}
	}
}