using UnityEngine;
using System.Collections;

public class AddForce : MonoBehaviour {

	public int horiSpeed;
	private Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.AddForce (horiSpeed, Random.Range(300,1000) , 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.magnitude < 300)
			rb.AddForce (Random.Range (-100, 100), Random.Range (-100, 100), 0);
		if (transform.position.Equals (new Vector3 (0, 0, 0))) {
			rb.velocity = new Vector3(0,0,0);
			rb.AddForce (horiSpeed, Random.Range(300,1000) , 0);
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.name == "paddle_0" || col.gameObject.name == "paddle_1")
			rb.AddForce (Random.Range (-100, 100), Random.Range (-100, 100), 0);
	}
}
