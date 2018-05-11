using UnityEngine;
using System.Collections;

public class GrinderScript : MonoBehaviour {

    //private Transform _transform;

	public GameObject _mainParent;

	public float _grinderSpeed=5.0f;

	void Awake()
	{
		GameObject _midParent = new GameObject ();
		transform.localScale = Vector3.one;
		_midParent.transform.parent = _mainParent.transform;
		transform.parent = _midParent.transform;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles += new Vector3 (0.0f,0.0f,Time.deltaTime *_grinderSpeed);
	}
}