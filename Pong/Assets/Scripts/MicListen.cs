using UnityEngine;
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
		startMic ();
	}
	private void startMic() {
		audio.clip = Microphone.Start(null, true, 1, 44100);
		while (!(Microphone.GetPosition(null) > 0)) {
		}
		audio.Play ();
		offset = 0;
	}
	
	// Update is called once per frame
	void Update () {
		AnalyzeSound();
		Debug.Log("rmsValue: "+rmsValue);
		Debug.Log("dbValue: "+dbValue);
		Debug.Log ("pitchValue: " + pitchValue);
		//float[] data = new float[100];
		//Debug.Log (clip.frequency);
		//	clip.GetData (data, offset);
		
		/*if (offset >= 16000) {
			Debug.Log("Volume: "+avg/1600f);
			offset = 0;
		}
		offset += 100;

		if (data == null) {
			Debug.Log("Failed to get Audio Data!!!");
			return;
		}
		ArrayList s = new ArrayList ();
		foreach (float f in data) {
			s.Add(Mathf.Abs(f));

		}
		s.Sort ();
		float volume = (float)s [5];
		avg += volume;
		//Debug.Log ("Volume: " + volume);
		/*
		var byteArray = new byte[data.Length * 4];
		Buffer.BlockCopy(data, 0, byteArray, 0, byteArray.Length);

		int volume = calculateRMSLevel (byteArray);
		Debug.Log ("Volume: "+volume);
		*/
	}
	
	private const int SAMPLECOUNT = 256;   // Sample Count.
	private const float REFVALUE = 0.1f;    // RMS value for 0 dB.
	private const float THRESHOLD = 0.02f;  // Minimum amplitude to extract pitch (recieve anything)
	
	public int clamp = 160;            // Used to clamp dB (I don't really understand this either).
	
	private float pitchValue;
	private float rmsValue;
	private float dbValue;
	
	private void AnalyzeSound() {
		float[] spectrum = new float[SAMPLECOUNT];
		// Get all of our samples from the mic.
		float[] samples = new float[SAMPLECOUNT];
		audio.GetOutputData(samples, 0);
		
		// Sums squared samples
		float sum = 0;
		for (int i = 0; i < SAMPLECOUNT; i++){
			sum += Mathf.Pow(samples[i], 2);
		}
		
		// RMS is the square root of the average value of the samples.
		rmsValue = Mathf.Sqrt(sum / (float) SAMPLECOUNT);
		dbValue = 20 * Mathf.Log10(rmsValue / REFVALUE);
		
		// Clamp it to {clamp} min
		if (dbValue < -clamp) {
			dbValue = -clamp;
		}
		
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
	
	
	private int calculateRMSLevel(byte[] data) {
		long lSum = 0;
		for (int i=0; i<data.Length; i++) {
			lSum = lSum + (long)data[i];
		}
		double dAvg = lSum / data.Length;
		
		double sumMeanSquare = 0;
		for(int j=0; j < data.Length; j++) {
			sumMeanSquare = sumMeanSquare + (double)(Mathf.Pow((float)(data[j] - dAvg), 2f));
		}
		double averageMeanSquare = sumMeanSquare / data.Length;
		return (int)(Mathf.Pow ((float)averageMeanSquare, 0.5f) + 0.5f);
	}
	
}
