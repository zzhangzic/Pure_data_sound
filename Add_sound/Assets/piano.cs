using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piano : MonoBehaviour {
	public AudioSource[] sourceList = new AudioSource[9];
	public AudioSource baseNote;
	public float[] playGap = new float[49];
	public int count = 0;
	public AudioSource[] playList = new AudioSource[50];
//	public List<int,int> scoreList = new List<int,int>();
	public int[][] Score;
	// Use this for initialization
	void Start () {
		baseNote = GameObject.Find ("pianoManager").GetComponent<AudioSource> ();
		for (int i = -4; i <= 4; i++) {
			sourceList [i + 4] = createNote (i);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			playList = randomSelection ();
			playGap = randomGap (playGap);
			StartCoroutine(playPiano (playList, playGap));
		}
		//Debug.Log (sourceList [0].pitch);
	}

	AudioSource createNote(int input){
		float pitch_offset = input;
		AudioSource result = baseNote;
		pitch_offset *= 2;
		result.pitch = Mathf.Pow (2f, pitch_offset / 12.0f);
		//Debug.Log (result.pitch);
		return Instantiate(result);
	}

	IEnumerator playPiano(AudioSource[] tempList,float[]gap){
		int counter = 0;
		while (tempList [counter] != null) {
			tempList [counter].Play ();
			yield return new WaitForSeconds (gap [counter]);
			counter++;
		}
	}

	AudioSource[] randomSelection(){
		Debug.Log ("hi");
		AudioSource[] temp = new AudioSource[49];
		for (int i = 0; i < temp.Length; i++) {
			temp [i] = sourceList [Random.Range (0,8)];
		}
		return temp;
	}

	IEnumerator playInsertGap(AudioSource[] tempList){
		foreach (AudioSource temp in tempList) {
			float waitSeconds = Random.Range (0f, ((temp.clip.length) + 2.0f));

			while (tempList [count].isPlaying) {
				yield return new WaitForSeconds (waitSeconds);
			}
			tempList [count].Play ();
		}
	}

	float[] randomGap(float[] temp){
		for (int i = 0; i < temp.Length; i++) {
			Debug.Log ("hi");
			temp[i] = Random.Range (0f, 0.6f);
		}
		return temp;
	}


			
}
