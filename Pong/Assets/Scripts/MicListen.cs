﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class MicListen : MonoBehaviour {
	
	private Microphone mic;
	//private AudioClip clip;
	private int offset;
	private float avg;
	private string device;
	private AudioSource audio;
	
	// Use this for initialization
	void Start () {
		
		audio = GetComponent<AudioSource> ();
		audio.clip = Microphone.Start(null, true, 1, 44100);
		while (!(Microphone.GetPosition(null) > 0)) {
		}
		audio.Play ();
	}

	private int sampleCount;
	private float sampleSum;
	// Update is called once per frame
	void Update () {
		AnalyzeSound();
		//Debug.Log ("pitchValue: " + pitchValue);
		if (pitchValue > 0) {
			sampleCount++;
			sampleSum += pitchValue;
			if(sampleCount >= 5) {
				float avg = sampleSum/sampleCount;
				sampleCount = 0;
				sampleSum = 0;
				Debug.Log("Average Pitch: "+avg);
			}
		} else {
			if(sampleCount > 0) {
				float avg = sampleSum/sampleCount;
				sampleSum = 0;
				sampleCount = 0;
				Debug.Log("Average Pitch: "+avg);
			}
		}
	}
	
	private const int SAMPLECOUNT = 256;   // Sample Count.
	private const float THRESHOLD = 0.02f;  // Minimum amplitude to extract pitch (recieve anything)
	
	private float pitchValue;
	
	private void AnalyzeSound() {
		float[] spectrum = new float[SAMPLECOUNT];
		
		// Gets the sound spectrum.
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0;
		int maxN = 0;
		
		// Find the highest sample.
		for (int i = 0; i < SAMPLECOUNT; i++){
			if (spectrum[i] > maxV  && spectrum[i] > THRESHOLD){
				maxV = spectrum[i];
				maxN = i; // maxN is the index of max
			}
		}
		
		// Pass the index to a float variable
		float freqN = (float) maxN;
		
		// Interpolate index using neighbours
		if (maxN > 0  && maxN < SAMPLECOUNT - 1) {
			float dL = spectrum[maxN-1] / spectrum[maxN];
			float dR = spectrum[maxN+1] / spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);
		}
		
		// Convert index to frequency
		pitchValue = freqN * 24000f / (float)SAMPLECOUNT;
	}
	
}
