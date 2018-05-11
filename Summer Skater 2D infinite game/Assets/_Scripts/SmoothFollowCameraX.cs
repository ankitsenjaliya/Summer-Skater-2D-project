using UnityEngine;
using System.Collections;

public class SmoothFollowCameraX : MonoBehaviour {

	public Transform lookAt;
	public bool smooth = true;
	private float smoothSpeed = 0.125f;
	public Vector3 offset;
	public TextMesh frameRate;

	// Use this for initialization
	void Start () {
		//Debug.Log(1/Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		//Application.targetFrameRate = 20;
		Vector3 dPosition = lookAt.transform.position + offset;
		if (smooth) {
			transform.position = Vector3.Lerp (transform.position,dPosition,smoothSpeed);
		} else {
			transform.position = dPosition;
		}
		//frameRate.text = "" + (int)1 / Time.deltaTime;
		//Debug.Log (Camera.current);
		//Debug.Log(1/Time.deltaTime);
	}
}