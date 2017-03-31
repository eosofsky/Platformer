using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour {

    public float startingX;
    public float startingY;
    public float startingZ;

    public float endingX;
    public float endingY;
    public float endingZ;

    private float time;
    private float journeyLength;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private int forwardBack;

    // Use this for initialization
    void Start()
    {
        forwardBack = -1;
        time = Time.time;

        startPoint = new Vector3(startingX, startingY, startingZ);
        endPoint = new Vector3(endingX, endingY, endingZ);
        journeyLength = Vector3.Distance(startPoint, endPoint);
    }
	
	// Update is called once per frame
	void Update () {
        var currentPosition = transform.position;
        
        if (AlmostEqual(currentPosition.x, startingX) &&
               AlmostEqual(currentPosition.y, startingY) &&
               AlmostEqual(currentPosition.z, startingZ))
        {
            forwardBack = -1;
            time = Time.time;
        }
        else if (AlmostEqual(currentPosition.x, endingX) &&
               AlmostEqual(currentPosition.y, endingY) &&
               AlmostEqual(currentPosition.z, endingZ))
        {
            forwardBack = 1;
            time = Time.time;
        }

        if (forwardBack == -1)
        {
            float distCovered = (Time.time - time) * 1.0F;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPoint, endPoint, fracJourney);

            transform.position = Vector3.Lerp(currentPosition, endPoint, 0.02f);
        }
        else if (forwardBack == 1)
        {
            transform.position = Vector3.Lerp(currentPosition, startPoint, 0.02f);
        }
    }
    
    /*
     *  Equality to determine location.
     */  
    bool AlmostEqual(float x, float y)
    {
        var buffer = 0.05f;
        var low = y - buffer;
        var high = y + buffer;

        if (low <= x && x <= high)
        {
            return true;
        }
        return false;
    }
}
