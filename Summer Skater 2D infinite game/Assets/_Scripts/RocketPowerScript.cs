using UnityEngine;
using System.Collections;

public class RocketPowerScript : MonoBehaviour {

	public static RocketPowerScript _instance;

	public bool _boyPassed = false;

	public BoxCollider _boxCollider;

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		_boxCollider = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			StartCoroutine ("RocketRepooling");
		}
	}

	public IEnumerator RocketRepooling()
	{
		_boxCollider.enabled = false;
		yield return new WaitForSeconds (15.0f);
		_boxCollider.enabled = true;
		_boyPassed = true;
	}
}