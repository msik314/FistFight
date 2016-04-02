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
	private Vector3 startPos;
	private Collider2D hitbox;

	// Use this for initialization
	void Awake ()
	{
		rbody = GetComponent<Rigidbody2D> ();
		locked = true;
		startPos = transform.position;
	}

	void FixedUpdate()
	{
		if (!locked)
		{
			Vector2 vel = new Vector2(speed * Input.GetAxisRaw(hAxis), speed * Input.GetAxisRaw(vAxis));
			rbody.velocity = vel;
			hitbox = GetComponent<Collider2D> ();
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Face") return;
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

	void Rest()
	{
		locked = true;
		rbody.velocity = Vector2.zero;
	}

	void Unlock()
	{
		locked = false;
	}

	public void reset()
	{
		StartCoroutine (returnToStart ());
	}

	IEnumerator returnToStart()
	{
		locked = true;
		hitbox.enabled = false;
		rbody.velocity = (startPos - transform.position) / (lockTime + 0.02f);
		yield return new WaitForSeconds (lockTime);
		locked = false;
		hitbox.enabled = true;
	}

	IEnumerator bounce()
	{
		locked = true;
		yield return new WaitForSeconds (lockTime);
		locked = false;
	}
}
