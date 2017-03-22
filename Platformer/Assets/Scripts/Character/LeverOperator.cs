using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverOperator : MonoBehaviour {

	public float leverRotAmount = 0.1f;
	public float leverRotMax = 0f;
	public float pushDelay;
	public float pullDelay;

	private Animator animator;
	private bool pushed = false;
	private GameObject leverHandle;
	private bool isPushing = false;
	private bool isPulling = false;
	private Quaternion startRot;

	void Awake () {
		animator = GetComponent<Animator> ();
		leverHandle = GameObject.Find ("Lever Handle");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			PushOrPullLever ();
		}

		if (isPushing) {
			Quaternion targetRot = leverHandle.transform.localRotation;
			targetRot.x = leverRotMax;
			leverHandle.transform.localRotation = Quaternion.Slerp (leverHandle.transform.localRotation, targetRot, leverRotAmount);
			if (AlmostEqual (leverHandle.transform.localRotation.x, targetRot.x)) {
				isPushing = false;
			}
		} else if (isPulling) {
			leverHandle.transform.localRotation = Quaternion.Slerp (leverHandle.transform.localRotation, startRot, leverRotAmount);
			if (AlmostEqual (leverHandle.transform.localRotation.x, startRot.x)) {
				isPulling = false;
			}
		}
	}


	public void PushOrPullLever () {
		if (pushed) {
			StartCoroutine (PullLever ());
		} else {
			StartCoroutine (PushLever ());
		}
		pushed = !pushed;
	}

	IEnumerator PullLever () {
		animator.SetTrigger ("Pull");
		yield return new WaitForSeconds (pullDelay);
		isPulling = true;
	}

	IEnumerator PushLever () {
		animator.SetTrigger ("Push");
		yield return new WaitForSeconds (pushDelay);
		startRot = leverHandle.transform.localRotation;
		isPushing = true;
	}

	private bool AlmostEqual(float x, float y)
	{
		float buffer = 0.01f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}

}
