using UnityEngine;
using System.Collections;

public class JustTesr : MonoBehaviour {

	public Rigidbody rb;
	public GameObject _player;
	// Use this for initialization
	void Start () {
		rb = _player.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector3 (5.0f,0.0f,0.0f);
	}
}