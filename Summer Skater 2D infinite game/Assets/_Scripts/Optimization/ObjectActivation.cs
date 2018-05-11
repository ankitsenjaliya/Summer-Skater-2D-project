using UnityEngine;
using System.Collections;

public class ObjectActivation : MonoBehaviour {
	
	public Transform _camObj;
	public Transform ownTransform;
			
	// Use this for initialization
	void Start () {
		ownTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		ownTransform.LookAt (_camObj);
	}
}