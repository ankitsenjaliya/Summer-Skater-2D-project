using UnityEngine;
using System.Collections;

public class ObjectOptimizer : MonoBehaviour {

	public int _optimizationNo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		switch(_optimizationNo)
		{
		case 1:
			col.gameObject.GetComponent<MeshRenderer> ().enabled = true;
			break;

		case 2:
			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;
			break;
		}
	}
}