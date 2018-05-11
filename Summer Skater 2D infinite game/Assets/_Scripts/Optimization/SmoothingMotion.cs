using UnityEngine;
using System.Collections;

public class SmoothingMotion : MonoBehaviour {

	public TextMesh _currentTimeStemp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//_currentTimeStemp.text = "" + Time.fixedDeltaTime;
	}

	public void IncreseTimeStemp()
	{
		Time.fixedDeltaTime += 0.001f;
	}

	public void DecreaseTimeStemp()
	{
		Time.fixedDeltaTime -= 0.001f;
	}
}