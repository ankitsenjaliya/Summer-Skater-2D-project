using UnityEngine;
using System.Collections;

public class SpikePendulum : MonoBehaviour {

	public GameObject _mainParent;

	// Use this for initialization
	void Start () {
		GameObject _midParent = new GameObject ();
		//transform.localScale = Vector3.one;
		_midParent.transform.parent = _mainParent.transform;
		transform.parent = _midParent.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
