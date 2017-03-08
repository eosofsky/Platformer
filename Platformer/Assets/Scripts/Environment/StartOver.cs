using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOver : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel("ConveyorDemo 1");
    }
}
