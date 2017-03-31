using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Level3Manager : MonoBehaviour {

	private ThirdPersonUserControl movementScript;

	void Awake () {
		movementScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThirdPersonUserControl> ();
		movementScript.enabled = false;
		Aim.Deactivate ();
		CutSceneManager.instance.BlackOut ();
	}

	void Start () {
		CutSceneManager.instance.ShowCutScene (7, 10, true, 3.0f, true, StartLevel); 
	}

	private void StartLevel () {
		movementScript.enabled = true;
		ThrowTear.Activate ();
		Aim.Activate ();
	}
}
