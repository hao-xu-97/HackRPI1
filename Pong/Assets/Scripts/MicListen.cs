using UnityEngine;
using System.Collections;

public class MicListen : MonoBehaviour {

	private Microphone mic;
	private AudioClip clip;

	// Use this for initialization
	void Start () {
		string[] devices = Microphone.devices;
		if (devices.Length > 0) {
			clip = Microphone.Start(devices[0], true, 1, 16000);
		} else {
			//Disable mic mode
		}
	}
	
	// Update is called once per frame
	void Update () {
		float[] data = new float[10];
		clip.GetData (data, 0);
		if (data == null) {
			Debug.Log("Failed to get Audio Data!!!");
			return;
		}
		int volume = calculateRMSLevel (data);
		Debug.Log ("Volume: "+volume);
	}

	private int calculateRMSLevel(float[] data) {
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
