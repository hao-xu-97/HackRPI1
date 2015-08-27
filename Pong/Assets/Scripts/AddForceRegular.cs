using UnityEngine;
using System.Collections;

public class AddForceRegular : MonoBehaviour {
	public int horiSpeed;
	private Rigidbody rb;
	
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.AddForce (horiSpeed, Random.Range(400,600) , 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.Equals (new Vector3 (0, 0, 0))) {
			rb.velocity = new Vector3(0,0,0);
			rb.AddForce (horiSpeed, Random.Range(400,600) , 0);
		}
	
	}
}
