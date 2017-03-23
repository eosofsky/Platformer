using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour {

    public float startingX;
    public float startingY;
    public float startingZ;

    public GameObject bucket;

    private bool moving;
    private int direction; // 1 if up, -1 if down, 0 if neither
    private int movements;

    private float time;

    private float height;
    private float delta; // the change between heights

	private GameObject DissembodiedHead;

    // Use this for initialization
    void Start()
    {
        time = 0;
        movements = 40; // how many steps we should take
        direction = 0;

        height = 2.0f;
        delta = (startingY - height) / movements;
        movements = 0; // set the amount of movements we've made to zero

		DissembodiedHead = GameObject.FindGameObjectWithTag ("Head");
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        var currentPosition = transform.position;

        if (!moving)
        {
            //DissembodiedHead.GetComponent<ThrowHead_V2>().LockMovement(false);
            if (direction == 0)
            {
                if (AlmostEqual(currentPosition.x, startingX) &&
                   AlmostEqual(currentPosition.y, startingY) &&
                   AlmostEqual(currentPosition.z, startingZ))
                {
                    direction = -1;
                }
            }
            else if (direction == -1)
            {
                direction = 1;
				DissembodiedHead.GetComponent<ThrowHead_V2> ().SetTarget (gameObject, true);
            }
            else if (direction == 1)
            {
                direction = 0;
				DissembodiedHead.GetComponent<ThrowHead_V2>().SetTarget (gameObject, false);
            }
            moving = true;
            time = 0.0f;
        }
        else
        {
            if (time > 5.0f) // five second loops
            {
                if (movements < 1)
                {
                    // GetComponentInParent<ConveyorSound>().PlaySound();
                }

                //DissembodiedHead.GetComponent<ThrowHead_V2>().LockMovement(true);
                if (direction == 0)
                {
                    SmoothRotate();
                }
                else
                {
                    SmoothRise();
                }
            }
        }
	}

    void SmoothRise()
    {
        if (movements < 40)
        {
            var currentPosition = transform.position;
            var x = currentPosition.x;
            var y = currentPosition.y - direction*delta;
            var z = currentPosition.z;


            transform.position = new Vector3(x, y, z);

            var axis = new Vector3(1.0f, 0.0f, 0.0f);
            var origin = bucket.transform.position;
            bucket.transform.RotateAround(origin, axis, direction * 0.9f);


            movements++;
        }
        else
        {
            movements = 0;
            moving = false;
        }
    }

    void SmoothRotate()
    {
        if (movements < 40)
        {
            var axis = new Vector3(0.0f, 1.0f, 0.0f);
            var origin = new Vector3(0.0f, 0.0f, 0.0f);
            transform.RotateAround(origin, axis, 0.5f);
            movements++;
        }
        else
        {
            movements = 0;
            moving = false;
        }
    }

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
