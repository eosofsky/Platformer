using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour {
    
    public float endingY;
    private float endingX;
    private float endingZ;

    private bool triggered;

    // Use this for initialization
    void Start () {
        endingX = transform.position.x;
        endingZ = transform.position.z;

        triggered = true;
	}
	
	// Update is called once per frame
	void Update () {
        var currentPosition = transform.position;

		if (triggered && currentPosition.y < endingY)
        {
            // Move Up
            Rise(currentPosition.y);
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

    public void Trigger()
    {
        // #TRIGGERED
        triggered = true;
    }
}
