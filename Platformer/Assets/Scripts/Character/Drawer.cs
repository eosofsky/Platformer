﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drawer : MonoBehaviour {

	private static Animator animator;
	private static NavMeshAgent agent;
	private static Transform internalStairsButton;
	private static bool isWalking = false;

	public Transform stairsButton;

	void Awake () {
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		internalStairsButton = stairsButton;
	}

	void Update () {
		if (isWalking) {
			if (AlmostEqualPos (transform.position, agent.destination)) {
				animator.SetBool ("Walking", false);
				isWalking = false;
				// Trigger cut scene, end of level 1
				Debug.Log ("Trigger cut scene, end of level 1");
			} else {
				agent.destination = Camera.main.transform.position;
			}
		}
	}

	/*public static void DrawerMoveToStairButton () {
		agent.destination = internalStairsButton.position;
		animator.SetBool ("Walking", true);
		isWalking = true;
	}*/

	public static void DrawerMoveToHead () {
		//GameObject player = GameObject.FindGameObjectWithTag ("Player");
		agent.destination = Camera.main.transform.position;
		animator.SetBool ("Walking", true);
		isWalking = true;
	}

	private bool AlmostEqualPos (Vector3 v1, Vector3 v2) {
		return (AlmostEqual (v1.x, v2.x) && AlmostEqual (v1.y, v2.y) && AlmostEqual (v1.z, v2.z));
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.5f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}
