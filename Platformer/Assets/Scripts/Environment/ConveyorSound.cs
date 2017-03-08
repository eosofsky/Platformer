using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSound : MonoBehaviour {

    public AudioClip rotateSound;
    AudioSource rotate;

    // Use this for initialization
    void Start () {
        rotate = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (Time.deltaTime < 0.5f)
        {
            rotate.PlayOneShot(rotateSound);
        }
    }
}
