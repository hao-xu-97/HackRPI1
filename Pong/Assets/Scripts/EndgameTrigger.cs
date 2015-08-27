using UnityEngine;
using System.Collections;

public class EndgameTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		GameObject.Find ("Sphere").transform.position = new Vector3 (0, 0, 0); 
		if (this.Equals(GameObject.Find ("detect_000")))
			GameObject.Find ("score_1").GetComponent<Increment>().increment ();
		if (this.Equals(GameObject.Find ("detect_001")))
			GameObject.Find ("score_0").GetComponent<Increment>().increment ();
	}


}