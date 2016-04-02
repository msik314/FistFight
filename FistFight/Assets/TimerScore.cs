using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimerScore : MonoBehaviour {
	[SerializeField] private List<GameObject> fists;
	[SerializeField] private FaceScript P1;
	[SerializeField] private FaceScript P2;
	[SerializeField] private float maxTime;
	private List<FistScript> fistscripts;
	private float timer;

	// Use this for initialization
	void Awake () {
		timer = maxTime + 3;
		fistscripts = new List<FistScript> ();
		foreach (GameObject f in fists)
		{
			fistscripts.Add (f.GetComponent<FistScript> ());
		}
		StartCoroutine (startMatch());
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			foreach (FistScript fist in fistscripts)
			{
				fist.SendMessage ("Rest");
			}
		}
		if (timer <= -10) Application.LoadLevel(0);
	}

	IEnumerator startMatch()
	{
		yield return new WaitForSeconds (3);
		foreach (FistScript fist in fistscripts)
		{
			fist.SendMessage ("Unlock");
		}
		timer = maxTime;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 20), "P1 Score: " + P2.getHits ().ToString ());
		GUI.Label (new Rect (Screen.width - 110, 10, 500, 20), "P2 Score: " + P1.getHits ().ToString ());
		if (timer >= maxTime) GUI.Label (new Rect (Screen.width / 2 - 15, 10, 30, 20), ((int)(timer - maxTime + 1)).ToString());
		else if (timer >= 0) GUI.Label (new Rect (Screen.width / 2 - 15, 10, 30, 20), ((int)(timer + 1)).ToString());
		else if (P1.getHits () > P2.getHits ())  GUI.Label (new Rect (Screen.width / 2 - 25, 10, 50, 20), "P2 Wins");
		else if (P2.getHits () > P1.getHits ())  GUI.Label (new Rect (Screen.width / 2 - 25, 10, 50, 20), "P1 Wins");
		else GUI.Label (new Rect (Screen.width / 2 - 20, 10, 40, 20), "Draw");
	}
}
