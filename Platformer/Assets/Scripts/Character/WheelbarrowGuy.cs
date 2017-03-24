using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WheelbarrowGuy : MonoBehaviour {

	public Transform miniFurnace;
	public Transform bigFurnace;
	public float delay;

	private Transform dest;
	private Animator animator;
	private NavMeshAgent agent;

	void Awake () {
		if (Random.value < 0.5f) {
			dest = bigFurnace;
		} else {
			dest = miniFurnace;
		}
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = dest.position;
		animator = GetComponent<Animator> ();
	}

	void Update () {
		if (AlmostEqualPos (transform.position, dest.position)) {
			ChangeDest (dest);
		}
	}

	private void ChangeDest (Transform currentDest) {
		if (currentDest == miniFurnace) {
			dest = bigFurnace;
		} else if (currentDest == bigFurnace) {
			dest = miniFurnace;
		}
		StartCoroutine (IdleAndGo ());
	}

	IEnumerator IdleAndGo () {
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.enabled = false;
		animator.SetBool ("Idling", true);
		yield return new WaitForSeconds (delay);
		animator.SetBool ("Idling", false);
		agent.enabled = true;
		agent.destination = dest.position;
	}

	private bool AlmostEqualPos (Vector3 v1, Vector3 v2) {
		return (AlmostEqual (v1.x, v2.x) && AlmostEqual (v1.y, v2.y) && AlmostEqual (v1.z, v2.z));
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.1f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}
