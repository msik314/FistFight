using UnityEngine;
using System.Collections;

public class FaceScript : MonoBehaviour {
	[SerializeField] private float FlinchTime;
	[SerializeField] private Sprite normal;
	[SerializeField] private Sprite flinch;
	private int hits;
	private SpriteRenderer sr;
	// Use this for initialization
	void Awake () {
		hits = 0;
		sr = GetComponent<SpriteRenderer> ();
		sr.sprite = normal;
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		++hits;
		StartCoroutine (hit ());
		col.gameObject.SendMessage ("reset");
	}


	IEnumerator hit()
	{
		sr.sprite = flinch;
		yield return new WaitForSeconds (FlinchTime);
		sr.sprite = normal;
	}

	public int getHits()
	{
		return hits;
	}
}
