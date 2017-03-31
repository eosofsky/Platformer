using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hit : MonoBehaviour {

	private GameObject DissembodiedHead;

	void Awake () {
		DissembodiedHead = GameObject.FindGameObjectWithTag ("Head");
	}
		
	void OnTriggerEnter (Collider collider) {
		Debug.Log (collider.name);
		if (string.Compare(collider.name, "DissembodiedHead") == 0) {
			GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;
			// Win
			SceneManager.LoadScene (3);
			//DissembodiedHead.GetComponent<ThrowHead_V3> ().SetTarget (GameObject.FindGameObjectWithTag ("Monarch Head").gameObject, false);
		}
	}
}
