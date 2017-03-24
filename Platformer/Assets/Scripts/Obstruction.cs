using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstruction : MonoBehaviour {

	public float removeAmount = 0.01f;

	private static bool isRemoving = false;
	private Image[] obstructions;

	void Awake () {
		obstructions = GetComponentsInChildren<Image> ();
	}

	void Update () {
		if (isRemoving) {
			for (int i = 0; i < obstructions.Length; i++) {
				Color currentColor = obstructions [i].color;
				Color targetColor = currentColor;
				targetColor.a = 0.0f;
				if (AlmostEqual (currentColor.a, targetColor.a)) {
					isRemoving = false;
				} else {
					obstructions [i].color = Color.Lerp (currentColor, targetColor, removeAmount);
				}
			}
		}
	}

	public static void RemoveObstruction () {
		isRemoving = true;
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.01f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}

}
