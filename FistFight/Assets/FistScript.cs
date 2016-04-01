using UnityEngine;
using System.Collections;

public class FistScript : MonoBehaviour {
	[SerializeField] private string hAxis;
	[SerializeField] private string vAxis;
	[SerializeField] private float speed;
	[SerializeField] private float recoil;
	[SerializeField] private float lockTime;
	private bool locked;
	private Rigidbody2D rbody;

	// Use this for initialization
	void Awake ()
	{
		rbody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		if (!locked)
		{
			Vector2 vel = new Vector2(speed * Input.GetAxisRaw(hAxis), speed * Input.GetAxisRaw(vAxis));
			rbody.velocity = vel;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		StartCoroutine (bounce());
		Vector2 normal = Vector2.zero;
		int counter = 0;
		foreach (ContactPoint2D c in col.contacts)
		{
			normal += c.normal;
			++counter;
		}
		normal /= counter;
		rbody.velocity = recoil * normal;
	}

	IEnumerator bounce()
	{
		locked = true;
		yield return new WaitForSeconds (lockTime);
		locked = false;
	}
}
