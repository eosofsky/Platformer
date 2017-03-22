using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour {
    
    public float endingY;
    private float endingX;
    private float endingZ;
	private float startingY;

    private bool rising = false;
	private bool falling = false;
	private bool iWantToRise = false;
	private bool blocked = false;

    void Start () {
        endingX = transform.position.x;
        endingZ = transform.position.z;
		startingY = transform.position.y;
	}
	
	void Update () {
        var currentPosition = transform.position;

		if (!blocked && iWantToRise) {
			Trigger ();
		}

		if (rising && currentPosition.y < endingY) {
			if (blocked) {
				rising = false;
				falling = true;
			} else {
				Rise (currentPosition.y);
			}
		}
		if (falling && currentPosition.y > startingY) {
			Fall (currentPosition.y);
		}
	}

    void Rise (float y)
    {
        var newPosition = new Vector3(
            endingX,
            y + 0.01f,
            endingZ);
        transform.position = newPosition;
    }

	void Fall (float y)
	{
		var newPosition = new Vector3(
			endingX,
			y - 0.01f,
			endingZ);
		transform.position = newPosition;
	}

    public void Trigger ()
    {
        // #TRIGGERED
		rising = true;
		falling = false;
    }

	public void Untrigger ()
	{
		falling = true;
		rising = false;
		iWantToRise = false;
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Coal Pile") {
			blocked = true;
			iWantToRise = true;
			falling = true;
			rising = false;
		}
	}

	void OnCollisionExit (Collision collision) {
		if (collision.gameObject.name == "Coal Pile") {
			blocked = false;
		}
	}
}
