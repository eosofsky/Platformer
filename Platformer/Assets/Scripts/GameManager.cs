using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public int maxShovelCount = 3;

	private ThirdPersonUserControl movementScript;
	private bool hasStarted = false;
	private int shovelCount = 0;

	void Awake () {
		instance = this;
		movementScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<ThirdPersonUserControl> ();
	}

	void Start () {
		CutSceneManager.instance.ShowCutScene (0, 1, false, 3.0f, false, StartGame);
		/* At start you can shovel, but you can't draw, aim, look around, or move */
		// 1
		Shovel.Activate ();
		Draw.Deactivate ();
		Aim.Deactivate ();
		//LookAround.Deactivate ();
		movementScript.enabled = false;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && hasStarted && shovelCount < maxShovelCount) {
			// 3
			shovelCount++;
			if (shovelCount == maxShovelCount) {
				// Drop crystal/tear
				Shard.Activate ();
			}
		}
	}

	void StartGame () {
		// 2
		hasStarted = true;
	}

	void ShowDrawerCutScene () {
		// 5
		/* During cut scene, you still cannot shovel, draw, or aim, also can't look around or move */
		movementScript.enabled = false;
		CutSceneManager.instance.ShowCutScene (1, 4, true, 3.0f, true, ShowInstructionCutScene);
	}

	void ShowInstructionCutScene () {
		// 6
		CutSceneManager.instance.ShowCutScene (4, 5, false, 3.0f, false, ActivateDrawer);
	}

	void ActivateDrawer () {
		// 7
		/* You still cannot shovel or look around, but you can draw, aim, and move */
		Draw.Activate ();
		ThrowHead_V2.SetPostThrow (Drawer.DrawerMoveToHead);
		Aim.Activate ();
		movementScript.enabled = true;
	}

	//void CoalHasCleared () {
	//	Drawer.DrawerMoveToStairButton ();
	//}

	public void ShardHit () {
		/* Now you cannot shovel, still can't draw or aim, but you can move and look around.
				   Obstruction disappears. */
		// 4
		Shovel.Deactivate ();
		//LookAround.Activate (ShowDrawerCutScene);
		LookAround.instance.Look (ShowDrawerCutScene);
		//movementScript.enabled = true;
		Obstruction.RemoveObstruction ();
	}

}
