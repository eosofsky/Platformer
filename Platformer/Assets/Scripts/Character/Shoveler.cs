using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shoveler : MonoBehaviour {

	private static Animator animator;
	private static NavMeshAgent agent;
	private static bool isWalking = false;

	public Transform coalPile;

	void Awake () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if (isWalking && AlmostEqualPos(transform.position, agent.destination)) {
			animator.SetBool ("Walking", false);
			isWalking = false;
		}
	}

	public void ShovelerMove () {
		agent.destination = coalPile.position;
		animator.SetBool ("Walking", true);
		isWalking = true;
	}

	private bool AlmostEqualPos (Vector3 v1, Vector3 v2) {
		return (AlmostEqual (v1.x, v2.x) && AlmostEqual (v1.y, v2.y) && AlmostEqual (v1.z, v2.z));
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.07f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}
