using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHands : MonoBehaviour {

	public static bool HasOriginalHands = true;

	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			Switch ();
		}
	}

	public static void Switch () {
		GameObject[] originalHands = GameObject.FindGameObjectsWithTag ("OriginalHand");
		for (int i = 0; i < originalHands.Length; i++) {
			originalHands [i].GetComponent <SkinnedMeshRenderer> ().enabled = !HasOriginalHands;
		}

		GameObject[] altHands = GameObject.FindGameObjectsWithTag ("AltHand");
		for (int i = 0; i < originalHands.Length; i++) {
			altHands [i].GetComponent <SkinnedMeshRenderer> ().enabled = HasOriginalHands;
		}

		HasOriginalHands = !HasOriginalHands;
	}
}
