using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drawer : MonoBehaviour {

	private static Animator animator;
	private static NavMeshAgent agent;
	private static Transform internalStairsButton;
	private static bool isWalking = false;
	private GameObject DissembodiedHead;

	public Transform stairsButton;

	void Awake () {
		DissembodiedHead = GameObject.FindGameObjectWithTag ("Head");
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
				CutSceneManager.instance.ShowCutScene (4, 7, true, 3.0f, true, PostCutscene);
			} else {
				agent.destination = Camera.main.transform.position;
			}
		}
	}

	private void PostCutscene () {
		DissembodiedHead.GetComponent<ThrowHead_V2> ().ResetHead ();
		DrawerMoveToStairButton ();

	}

	public static void DrawerMoveToStairButton () {
		agent.destination = internalStairsButton.position;
		animator.SetBool ("Walking", true);
		isWalking = true;
	}

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
