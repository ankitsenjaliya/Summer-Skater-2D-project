using UnityEngine;
using System.Collections;

public class Repositioning : MonoBehaviour {

	public Camera _uiCamera;

	private float _ratio,_screenWidth,_screenHeight;

	private float _ratioFactor;

	private Vector3 _v1, _v2, v3;

	private float _originalWidth = 1920f, _originalHeight = 1080f;

	// Use this for initialization
	void Start () {

		_screenWidth =  Screen.width;
		_screenHeight = Screen.height;

		_ratio = _screenWidth / _screenHeight;

		//Debug.Log ("Ratio:"+ _ratio);

		//_ratioFactor = _ratio / 1.25f;

	//	_v1 = transform.position * _ratioFactor;

	//	transform.position = _v1;	

	//	Debug.Log ("_v1:"+_v1);
	//	Debug.Log ("_v2:"+_v2);

		gameObject.GetComponent<Camera> ().aspect = (_originalWidth / _originalHeight) * (_ratio);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}