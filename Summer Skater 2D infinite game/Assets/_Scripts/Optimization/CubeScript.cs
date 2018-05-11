using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

	private Rigidbody _rb;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		_rb.velocity = new Vector3 (10.0f,0.0f,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		
	}
}