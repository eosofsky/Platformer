using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour {

	public float height = 0.5f;
	public float raiseAmount = 0.1f;

	private bool isRaising;
	private bool isFalling;
	private float start;

	void Start () {
		isRaising = false;
		isFalling = false;
		start = transform.position.y;
	}

	void Update () {
		Vector3 dest = transform.position;
		if (isRaising) {
			dest.y = height;
		} else if (isFalling) {
			dest.y = start;
		} else {
			return;
		}
		transform.position = Vector3.Lerp (transform.position, dest, raiseAmount);
		if (AlmostEqual (transform.position.y, dest.y)) {
			isRaising = false;
			isFalling = false;
		}
	}

	public void MoveUp () {
		isRaising = true;
		isFalling = false;
	}

	public void MoveDown () {
		isFalling = true;
		isRaising = false;
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.001f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}
