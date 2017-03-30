using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWheelbarrow : MonoBehaviour {

	private GameObject DissembodiedHead;

	void Awake () {
		DissembodiedHead = GameObject.FindGameObjectWithTag ("Head");
	}

	public void SetTarget (bool makeTarget) {
		DissembodiedHead.GetComponent<ThrowHead_V2> ().SetTarget (gameObject.transform.GetChild (0).gameObject, makeTarget);
	}
}
