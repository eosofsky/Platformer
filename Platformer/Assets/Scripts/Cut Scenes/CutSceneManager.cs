using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour {

	public delegate void Callback ();

	public static CutSceneManager instance;
	public Sprite[] images;
	//public float switchDelay = 2.0f;
	public float fadeAmount = 0.01f;
	public float fadeDelay = 0.01f;

	private Image myImage;
	private Image backingImage;
	private bool waitingForDismiss = false;
	private Callback currentCallback = null;

	void Awake () {
		instance = this;
		myImage = gameObject.GetComponent<Image> ();
		backingImage = GameObject.Find ("Backing Image").GetComponent<Image> ();
	}

	public void BlackOut () {
		Color color = backingImage.color;
		color.a += fadeAmount;
		color.a = 1.0f;
		backingImage.color = color;
	}

	public void ShowCutScene (int start, int end, bool backing, float switchDelay, bool automaticDismiss, Callback callback) {
		currentCallback = callback;
		StartCoroutine (RunSequence (start, end, backing, switchDelay, automaticDismiss));
	}

	void Update () {
		if (waitingForDismiss && Input.GetMouseButtonDown (0)) {
			waitingForDismiss = false;
			Dismiss ();
		}
	}

	private IEnumerator RunSequence (int start, int end, bool backing, float switchDelay, bool automaticDismiss) {
		if (backing) {
			StartCoroutine (FadeInBacking ());
		}
		for (int i = start; i < end; i++) {
			if (i != end - 1) {
				yield return StartCoroutine (FadeImages (images [i]));
				yield return new WaitForSeconds (switchDelay);
			} else {
				yield return StartCoroutine (FadeImages (images [i]));
				if (automaticDismiss) {
					yield return new WaitForSeconds (switchDelay);
				}
			}
		}
		if (automaticDismiss) {
			Dismiss ();
		} else {
			waitingForDismiss = true;
		}
	}

	private void Dismiss () {
		StartCoroutine (FadeImages (null));
		StartCoroutine (FadeOutBacking ());
		if (currentCallback != null) {
			currentCallback ();
		}
	}

	private IEnumerator FadeInBacking () {
		int numIters = (int)(1.0f / fadeAmount + 1.0f);
		for (int i = 0; i < numIters; i++) {
			Color color = backingImage.color;
			color.a += fadeAmount;
			color.a = (color.a > 1.0f ? 1.0f : color.a);
			backingImage.color = color;
			yield return new WaitForSeconds (fadeDelay);
		}
	}

	private IEnumerator FadeOutBacking () {
		int numIters = (int)(1.0f / fadeAmount + 1.0f);
		for (int i = 0; i < numIters; i++) {
			Color color = backingImage.color;
			color.a -= fadeAmount;
			color.a = (color.a < 0.0f ? 0.0f : color.a);
			backingImage.color = color;
			yield return new WaitForSeconds (fadeDelay);
		}
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
