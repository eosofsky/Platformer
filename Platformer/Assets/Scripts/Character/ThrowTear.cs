using UnityEngine;
using System.Collections;

// Adapted from https://forum.unity3d.com/threads/throw-an-object-along-a-parabola.158855/
public class ThrowTear : MonoBehaviour
{
	public float firingAngle = 15.0f;
	public float gravity = 9.8f;
	public GameObject tear;

	private int count;
	private GameObject projectile;
	private bool isThrowing;
	private static bool isActive;

	void Awake () {
		count = 0;
		isThrowing = false;
		isActive = false;
	}

	public static void Activate () {
		isActive = true;
	}

	void Update()
	{
		if (!isActive) {
			return;
		}

		count++;
		if (count > 100) {
			if (!isThrowing) {
				projectile = Instantiate (tear);
				Vector3 start = transform.position;
				start.y += 0.1f;
				projectile.transform.position = start;
				isThrowing = true;
			} else {
				isThrowing = false;
			}
				
				//StartCoroutine (SimulateProjectile (GameObject.FindGameObjectWithTag ("Player").transform));

			//var rb = projectile.AddComponent<Rigidbody> ();
			//rb.useGravity = false;
			count = 0;
			return;
		}
		if (isThrowing) {
			projectile.transform.position = Vector3.Lerp (projectile.transform.position,
				GameObject.FindGameObjectWithTag ("Player").transform.position, 0.1f);
		}
	}

/*	IEnumerator SimulateProjectile(Transform target)
	{
		// Calculate distance to target
		float target_Distance = Vector3.Distance(projectile.transform.position, target.position);

		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;

		float elapse_time = 0;

		while (elapse_time < flightDuration)
		{
			projectile.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;

			yield return null;
		}
	}*/

	private bool AlmostEqualPos (Vector3 v1, Vector3 v2) {
		return (AlmostEqual (v1.x, v2.x) && AlmostEqual (v1.y, v2.y) && AlmostEqual (v1.z, v2.z));
	}

	private bool AlmostEqual (float x, float y)
	{
		float buffer = 0.1f;
		float low = y - buffer;
		float high = y + buffer;

		return (low <= x && x <= high);
	}
}