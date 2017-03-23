using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour {

	public delegate void Callback ();

	private static bool canLook = false; 
	private Animator animator;
	private static Callback postLookBehavior;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (canLook && Input.GetMouseButtonDown (0)) {
			StartCoroutine (StartLookAround ());
		}
	}

	private IEnumerator StartLookAround () {
		canLook = false;
		animator.SetTrigger ("LookAround");
		yield return new WaitForSeconds (1f);
		postLookBehavior ();
	}

	public static void Activate (Callback callback) {
		postLookBehavior = callback;
		canLook = true;
	}

	public static void Deactivate () {
		canLook = false;
	}

}
