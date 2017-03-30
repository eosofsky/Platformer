using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour {

	public delegate void Callback ();

	public static LookAround instance;

	//private static bool canLook = false; 
	private Animator animator;
	//private static Callback postLookBehavior;

	void Awake () {
		instance = this;
		animator = GetComponent<Animator> ();
	}

	/*void Update () {
		if (canLook && Input.GetMouseButtonDown (0)) {
			StartCoroutine (StartLookAround ());
		}
	}*/

	public void Look (Callback callback) {
		StartCoroutine (StartLookAround (callback));
	}

	private IEnumerator StartLookAround (Callback callback) {
		//canLook = false;
		animator.SetTrigger ("LookAround");
		yield return new WaitForSeconds (1f);
		callback ();
		//postLookBehavior ();
	}

	/*public static void Activate (Callback callback) {
		postLookBehavior = callback;
		canLook = true;
	}

	public static void Deactivate () {
		canLook = false;
	}*/

}
