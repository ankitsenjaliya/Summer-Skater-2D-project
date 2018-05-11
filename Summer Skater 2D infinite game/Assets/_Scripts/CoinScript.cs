using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	public GameObject[] _coinsArray;
	// Use this for initialization
	void Start () {	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResetAll()
	{
		for (int i = 0; i < _coinsArray.Length; i++) {
			_coinsArray [i].SetActive (true);
			//_coinsArray [i].GetComponent<BoxCollider> ().enabled = true;
		}
	}
}