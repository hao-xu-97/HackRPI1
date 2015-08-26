using UnityEngine;
using System.Collections;

public class InputListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 4 && Input.GetAxis ("Vertical") > 0)
			transform.position += new Vector3(0,Input.GetAxis ("Vertical"),0);
		if (transform.position.y > -4 && Input.GetAxis ("Vertical") < 0)
			transform.position += new Vector3(0,Input.GetAxis ("Vertical"),0);

	}
}
