using UnityEngine;
using System.Collections;

public class EndgameTriggerLeft : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		GameObject.Find ("Sphere").transform.position = new Vector3 (0, 0, 0); 
		GameObject.Find ("score_1").GetComponent<Increment>().increment ();
	}


}