using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Sphere").transform.position.y > this.transform.position.y)
			transform.position += new Vector3 (0, 0.175f, 0);
		if (GameObject.Find ("Sphere").transform.position.y < this.transform.position.y)
			transform.position -= new Vector3 (0, 0.175f, 0);

	}
}
