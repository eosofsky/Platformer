using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour {

	public Sprite[] images;
	public float switchDelay = 2.0f;
	public float fadeAmount = 0.01f;
	public float fadeDelay = 0.01f;

	private Image myImage;

	void Awake () {
		myImage = gameObject.GetComponent<Image> ();
	}

	void Start () {
		StartCoroutine (RunSequence ());
	}

	private IEnumerator RunSequence () {
		for (int i = 0; i < images.Length; i++) {
			yield return StartCoroutine (FadeImages (images[i]));
		   	yield return new WaitForSeconds (switchDelay);
		}
		gameObject.GetComponentInParent <Canvas> ().enabled = false;
	}

	private IEnumerator FadeImages (Sprite nextImage) {
		/* Fade first image out */
		int numIters = (int)(1.0f / fadeAmount + 1.0f);
		for (int i = 0; i < numIters; i++) {
			Color color = myImage.color;
			color.a -= fadeAmount;
			color.a = (color.a < 0.0f ? 0.0f : color.a);
			myImage.color = color;
			yield return new WaitForSeconds (fadeDelay);
		}

		/* Fade second image in (if it exists) */
		if (nextImage) {
			myImage.sprite = nextImage;
			for (int i = 0; i < numIters; i++) {
				Color color = myImage.color;
				color.a += fadeAmount;
				color.a = (color.a > 1.0f ? 1.0f : color.a);
				myImage.color = color;
				yield return new WaitForSeconds (fadeDelay);
			}
		}
	}
}
