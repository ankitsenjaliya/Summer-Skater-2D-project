using UnityEngine;
using System.Collections;

public class Rigidcube : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//rb.velocity = new Vector3 (10.0f,0.0f,0.0f);
		rb.MovePosition(transform.position + transform.right * Time.deltaTime);
	}
}