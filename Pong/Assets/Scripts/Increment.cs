using UnityEngine;
using System.Collections;

public class Increment : MonoBehaviour {
	private int currentScore;
	private Renderer rend;

	public Texture s0;
	public Texture s1;
	public Texture s2;
	public Texture s3;
	public Texture s4;
	public Texture s5;
	public Texture s6;
	public Texture s7;
	public Texture s8;
	public Texture s9;

	private Texture[] score;

	// Use this for initialization
	void Start () {
		score = new Texture[]{s0,s1,s2,s3,s4,s5,s6,s7,s8,s9};
		currentScore = 0;
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.material.mainTexture = score [0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void increment(){
		if (currentScore < 9) {
			currentScore++;
			rend.material.SetTexture (0, score [currentScore]);
		} else {
			currentScore = 0;
			rend.material.mainTexture = score [0];
		}
	}
}
