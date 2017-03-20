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

	void Awake () {
		Instance = this;
	}

	public void Activate()
	{
		m_Camera = Camera.main;
		isActive = true;
		isActivating = true;
		m_MouseLook.SetCursorLock (true);
	}

	public void Deactivate () {
		m_Camera = null;
		isActive = false;
		isActivating = false;
		m_MouseLook.SetCursorLock (false);
	}

	void Update () {
		if (!isActive) {
			return;
		}

		if (isActivating) {
			/* Slowly rotate camera to look towards body */
			GameObject body = GameObject.FindGameObjectWithTag ("Player");
			Quaternion targetRot = Quaternion.LookRotation(body.transform.position - m_Camera.transform.position);
			m_Camera.transform.localRotation = Quaternion.Slerp (m_Camera.transform.localRotation, targetRot,
					0.025f); 
			if (Quaternion.Angle (m_Camera.transform.localRotation, targetRot) < 5f) {
				isActivating = false;
				m_MouseLook.Init(m_Camera.transform, m_Camera.transform);
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
