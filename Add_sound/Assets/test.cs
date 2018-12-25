//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;

public class test : MonoBehaviour {
	public float Volume;
	public int Hdirection;
	public int Vdirection;
	private string device;
	private AudioClip speechData;
	private float[] data;
	private AudioSource myAudioSource;
	private string[] keyWords = new string[]{"up","left","right","down","shit","one","two","three","four","five","six","seven","up one","up two","up three","up four","right one","right two","right three","right four","left one","left two","left three","left four","down one","down two","down three","down four"};
	private KeywordRecognizer m_Recognizer;
	// Use this for initialization
	void Start () {
		myAudioSource = this.GetComponent<AudioSource> ();
		device = Microphone.devices[0];
		speechData = Microphone.Start (device, true, 10, 44100);
		myAudioSource.clip = speechData;
		while (!(Microphone.GetPosition (null) > 0)) {
		}
		//Debug.Log (Microphone.devices[0].isRecording);
		myAudioSource.Play ();

		m_Recognizer = new KeywordRecognizer (keyWords);
		m_Recognizer.OnPhraseRecognized += m_recognize_event;
		m_Recognizer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (getVolume ());
		//Debug.Log ();
	}

	void getSample(){
		
	}

	float getVolume(){
		float result = 0;
		float[] waveFormData = new float[128];
		int waveOffSet = Microphone.GetPosition (device) - 128 + 1;
		if (waveOffSet < 0) {
			return 0;
		}
		speechData.GetData (waveFormData, waveOffSet);
		for (int i = 0; i < 128; i++) {
			float VolumeMax = waveFormData [i];
			if (result < VolumeMax) {
				VolumeMax = result;
			}
		}
		return result;
	}

	private void m_recognize_event(PhraseRecognizedEventArgs Args){
		
		Debug.Log ("keyword: " + Args.text + " confidence level: " + Args.confidence.ToString());
		switch (Args.text) {
		case "up":
			Vdirection = 1;
			break;
		case "down":
			Vdirection = -1;
			break;
		case "left":

			Hdirection = -1;
			break;
		case "right":

			Hdirection = 1;
			break;
		case "up one":

			Vdirection = 1;
			break;
		case "up two":

			Vdirection = 2;
			break;
		case "up three":

			Vdirection = 3;
			break;
		case "up four":

			Vdirection = 4;
			break;
		case "down one":

			Vdirection = -1;
			break;
		case "down two":

			Vdirection = -2;
			break;
		case "down three":

			Vdirection = -3;
			break;
		case "down four":

			Vdirection = -4;
			break;
		case "left one":

			Hdirection = -1;
			break;
		case "left two":

			Hdirection = -2;
			break;
		case "left three":

			Hdirection = -3;
			break;
		case "left four":

			Hdirection = -4;
			break;
		case "right one":

			Hdirection = 1;
			break;
		case "right two":

			Hdirection = 2;
			break;
		case "right three":

			Hdirection = 3;
			break;
		case "right four":

			Hdirection = 4;
			break;
		}
	}
}



