using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsButton : MonoBehaviour {

	public Material activateMat;
	public Material deactivateMat;

	private GameObject[] stairs;
	private static int numHolders = 0;
	private MeshRenderer meshRenderer;

	void Awake () {
		meshRenderer = GetComponent<MeshRenderer> ();
		stairs = GameObject.FindGameObjectsWithTag ("Stair");
	}

	void OnTriggerEnter () {
		numHolders++;
		if (numHolders == 1) {
			meshRenderer.material = activateMat;
			StairTrigger.instance.PlayStairSound ();
			for (int i = 0; i < stairs.Length; i++) {
				stairs[i].GetComponent<StairController>().Trigger();
			}
		}
	}
		
	void OnTriggerExit () {
		numHolders--;
		if (numHolders == 0) {
			meshRenderer.material = deactivateMat;
			StairTrigger.instance.PlayStairSound ();
			for (int i = 0; i < stairs.Length; i++) {
				stairs[i].GetComponent<StairController>().Untrigger();
			}
		}
	}

}
