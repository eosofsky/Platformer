using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsButton : MonoBehaviour {

	public Material activateMat;
	public Material deactivateMat;

	private GameObject[] stairs;
	private static int numHolders;
	private MeshRenderer meshRenderer;

	void Awake () {
		meshRenderer = GetComponent<MeshRenderer> ();
		meshRenderer.material = deactivateMat;
		stairs = GameObject.FindGameObjectsWithTag ("Stair");
		numHolders = 0;
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

	public static bool IsCovered () {
		return (numHolders > 0);
	}

}
