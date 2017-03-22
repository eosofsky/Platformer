using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WheelbarrowGuy : MonoBehaviour {

	public Transform coal;
	public Transform furnace;
	public float delay;

	private Transform dest;
	private Animator animator;

	void Awake () {
		dest = furnace;
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.destination = dest.position;

		animator = GetComponent<Animator> ();
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.transform == coal) {
			dest = furnace;
		} else if (collision.transform == furnace) {
			dest = coal;
		} else {
			return;
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

}
