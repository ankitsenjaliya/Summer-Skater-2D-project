using UnityEngine;
using System.Collections;

public class RandomNoGen : MonoBehaviour {


	public TextMesh t;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Generation ();
		}
	}

	public void Generation()
	{
		int rno = Random.Range (1,7);

		t.text = "" + rno;
	}
}
