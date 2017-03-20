using UnityEngine;
using System.Collections;

// Adapted from https://forum.unity3d.com/threads/throw-an-object-along-a-parabola.158855/
public class ThrowHead_V2 : MonoBehaviour
{
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;

    private bool hasThrown;
    private bool moving;
	private float cameraHeight = 1.3f;

	private Transform originalTransform;
	private Vector3 originalCameraPos;
	private Quaternion originalCameraRot;

    void Awake()
    {
        hasThrown = false;
        moving = true;
    }

    void Update()
    {
		if (!hasThrown) {
			if (Input.GetKeyDown (KeyCode.P)) {
				Transform target = Aim.Target;
				if (!target) {
					return;
				}

				originalCameraPos = Camera.main.transform.localPosition;
				originalCameraRot = Camera.main.transform.localRotation;

				/* Unparent the head from the body */
				originalTransform = Projectile.transform.parent;
				Projectile.transform.parent = null;

				/* Trigger animation */
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Animator> ().SetTrigger ("Throw");

				StartCoroutine (SimulateProjectile (target));

				var rb = gameObject.AddComponent<Rigidbody> ();
				rb.useGravity = false;
			}
		} else {
			if (Input.GetKeyDown (KeyCode.P)) {
				ResetHead ();
			}
		}
    }

	IEnumerator SimulateProjectile(Transform target)
    {
		yield return new WaitForSeconds (0.6f);

		/* Make old head invisible */
		GameObject.FindGameObjectWithTag ("AttachedHead").GetComponent<SkinnedMeshRenderer> ().enabled = false;

		/* Make the head visible */
		Projectile.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = true;

		Vector3 destination = target.position;
		destination.y -= 0.91f;
		destination.z -= 0.2f;

        // Move projectile to the position of throwing object + add some offset if needed.
        //Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
		float target_Distance = Vector3.Distance(Projectile.position, destination);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
		Quaternion orgRot = Camera.main.transform.rotation;
		Projectile.rotation = Quaternion.LookRotation(destination - Projectile.position);
		Quaternion newRot = Camera.main.transform.rotation;
		Camera.main.transform.rotation = orgRot;

        float elapse_time = 0;

		while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			/* Slowly rotate camera to have same rotation as head */
			Camera.main.transform.rotation = Quaternion.Lerp (Camera.main.transform.rotation, newRot, 0.025f);

			/* Slowly move camera to have same position as head */
			Vector3 cameraDest = Projectile.position;
			cameraDest.y += cameraHeight;
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, cameraDest, 0.01f);

			FadeHead ();

            elapse_time += Time.deltaTime;

            yield return null;
        }

		/* Make the head invisible */
		Projectile.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;

		/* Activate the FPS viewpoint */
		CameraRotate.Instance.Activate ();

		/* Make sure the head hasn't missed the platform if the platform has started moving upwards */
		if (Camera.main.transform.position.y < target.position.y) {
			gameObject.GetComponent <Rigidbody> ().useGravity = true;
		}

        gameObject.transform.parent = target;
        hasThrown = true;
		Aim.AimEnabled = false;
    }

	public void SetTarget (GameObject t, bool ct)
    {
		if (ct) {
			t.layer = LayerMask.NameToLayer("Targetable");
		} else {
			t.layer = LayerMask.NameToLayer("Default");
		}
    }

	private void FadeHead () {
		Material material = GameObject.FindGameObjectWithTag ("Head").GetComponentInChildren<SkinnedMeshRenderer> ().material;
		Color color = material.color;
		color.a -= 0.017f;
		material.color = color;
	}

	private void ResetHeadOpacity () {
		Material material = GameObject.FindGameObjectWithTag ("Head").GetComponentInChildren<SkinnedMeshRenderer> ().material;
		Color color = material.color;
		color.a = 1f;
		material.color = color;
	}

    public void LockMovement (bool moving)
    {
        this.moving = moving;
    }

	public void ResetHead () {
		if (!hasThrown) {
			return;
		}

		GameObject attachedHead = GameObject.FindGameObjectWithTag("AttachedHead");

		/* Reset the position and rotation */
		Projectile.transform.position = attachedHead.transform.position;
		Camera.main.transform.localPosition = originalCameraPos;
		Camera.main.transform.localRotation = originalCameraRot;

		/* Reparent the head to the body */
		Projectile.transform.parent = originalTransform;
		originalTransform = null;

		/* Make the dissembodied head invisible */
		Projectile.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = false;

		ResetHeadOpacity ();

		/* Make attached head visible */
		attachedHead.GetComponent<SkinnedMeshRenderer> ().enabled = true;

		/* Dissembodied head faces the same direction as the attached head */
		Projectile.rotation = attachedHead.transform.rotation;

		Destroy(gameObject.GetComponent<Rigidbody> ());

		CameraRotate.Instance.Deactivate ();

		hasThrown = false;
		Aim.AimEnabled = true;
	}
}