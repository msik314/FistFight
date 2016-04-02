using UnityEngine;
using System.Collections;

public class FaceScript : MonoBehaviour {
	[SerializeField] private float FlinchTime;
	private int hits;
	// Use this for initialization
	void Awake () {
		hits = 0;
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		++hits;
		col.gameObject.SendMessage ("reset");
	}


	IEnumerator hit()
	{
		yield return new WaitForSeconds (FlinchTime);
	}

	public int getHits()
	{
		return hits;
	}
}
