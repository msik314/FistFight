using UnityEngine;
using System.Collections;

public class SelectorScript : MonoBehaviour {
	[SerializeField] private Vector3 topPos;
	[SerializeField] private Vector3 botPos;
	[SerializeField] private string inputAxis;
	[SerializeField] private string selectAxis;
	private bool top;


	// Use this for initialization
	void Awake () {
		transform.position = topPos;
		top = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw (inputAxis) > 0.5) {
			top = true;
			transform.position = topPos;
		}

		if (Input.GetAxisRaw (inputAxis) < -0.5) {
			top = false;
			transform.position = botPos;
		}

		if (Input.GetAxisRaw (selectAxis) > 0.5) {
			if (top) {
				Application.LoadLevel (1);
			} else {
				Application.Quit ();
			}
		}
	}
}
