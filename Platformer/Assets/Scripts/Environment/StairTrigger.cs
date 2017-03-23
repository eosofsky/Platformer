using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour {

    //public GameObject head;
    //public GameObject[] stairs;

	public static StairTrigger instance;
    public AudioClip riseSound;
    AudioSource rise;

    private void Awake()
    {
		instance = this;
        rise = GetComponent<AudioSource>();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Head"))
        {
            rise.PlayOneShot(riseSound);
            foreach (var stair in stairs)
            {
                stair.GetComponent<StairController>().Trigger();
            }
        }
    }*/

	public void PlayStairSound () {
		rise.PlayOneShot(riseSound);
	}
}
