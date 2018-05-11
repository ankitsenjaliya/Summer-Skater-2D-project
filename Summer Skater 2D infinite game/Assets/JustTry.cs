using UnityEngine;
using System.Collections;

public class JustTry : MonoBehaviour {

	public GameObject _txtObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateText()
	{
		_txtObject.SetActive (true);
	}
}