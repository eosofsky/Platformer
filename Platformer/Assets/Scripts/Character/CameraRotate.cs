using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraRotate: MonoBehaviour {

	public static CameraRotate Instance;

	[SerializeField] private MouseLook m_MouseLook;
	private Camera m_Camera;

	private bool isActive = false;
	private bool isActivating = false;
	private int activatingCount = 0;

	void Awake () {
		Instance = this;
	}

	public void Activate()
	{
		m_Camera = Camera.main;
		isActive = true;
		isActivating = true;
	}

	void Update () {
		if (!isActive) {
			return;
		}

		if (isActivating) {
			/* Slowly rotate camera to look towards body */
			GameObject body = GameObject.FindGameObjectWithTag ("Player");
			Quaternion targetRot = Quaternion.LookRotation(body.transform.position - Camera.main.transform.position);
			Camera.main.transform.localRotation = Quaternion.Slerp (Camera.main.transform.localRotation, targetRot,
					0.025f); 
			activatingCount++;
			if (activatingCount >= 150) {
				isActivating = false;
				m_MouseLook.Init(m_Camera.transform , m_Camera.transform);
			}
			return;
		}

		RotateView();
	}

	private void RotateView()
	{
		m_MouseLook.LookRotation (transform, m_Camera.transform);
	}
}
