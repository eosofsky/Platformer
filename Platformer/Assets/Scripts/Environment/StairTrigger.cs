using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour {

    public GameObject head;
    public GameObject[] stairs;

    public AudioClip riseSound;
    AudioSource rise;

    private void Awake()
    {
        rise = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Head"))
        {
            rise.PlayOneShot(riseSound);
            foreach (var stair in stairs)
            {
                stair.GetComponent<StairController>().Trigger();
            }
        }
    } 
}
