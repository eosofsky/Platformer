using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsButton : MonoBehaviour {

	public Material activateMat;
	public Material deactivateMat;

	private static int numHolders = 0;

	private MeshRenderer renderer;

	void Awake () {
		renderer = GetComponent<MeshRenderer> ();
	}

	void OnTriggerEnter () {
		renderer.material = activateMat;
		numHolders++;
	}
		
	void OnTriggerExit () {
		numHolders--;
		if (numHolders == 0) {
			renderer.material = deactivateMat;
		}
	}

}
