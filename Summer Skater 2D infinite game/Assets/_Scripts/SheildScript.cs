using UnityEngine;
using System.Collections;

public class SheildScript : MonoBehaviour {

	public GameObject[] allObstaclesArray;

	private MeshRenderer shieldOwnMesh;

	public GameObject _shieldGlobe;

	public Material _shieldMaterial;

	//s	private Color32 defaultColor;	

	// Use this for initialization
	void Start () {
		shieldOwnMesh = this.gameObject.GetComponent<MeshRenderer> ();
	//	defaultColor = _shieldGlobe.GetComponent<Renderer> ().material.color;
	//	_color1 = new Color32 (defaultColor.r,defaultColor.g,defaultColor.b,121);
		//_color2 = new Color32 (defaultColor.r,defaultColor.g,defaultColor.b,85);

		_shieldMaterial = _shieldGlobe.gameObject.GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
		//	Debug.Log ("Player Shield Activated");
			ShieldActivated ();
		}
	}

	public void ShieldActivated()
	{
		//this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		shieldOwnMesh.enabled = false;
		for (int i = 0; i < allObstaclesArray.Length; i++) {
			BoxCollider[] colliders = allObstaclesArray [i].gameObject.GetComponents<BoxCollider> ();
			foreach (BoxCollider bc in colliders) {
				bc.enabled = false;
			}
		}
		StartCoroutine ("ResetProcesure");
	}

	IEnumerator ResetProcesure()
	{
		_shieldGlobe.GetComponent<MeshRenderer> ().enabled = true;
		StartCoroutine ("ShieldColorTransition");
		yield return new WaitForSeconds (10.0f);
		shieldOwnMesh.enabled = true;
		for (int i = 0; i < allObstaclesArray.Length; i++) {
			BoxCollider[] colliders = allObstaclesArray [i].gameObject.GetComponents<BoxCollider> ();
			foreach (BoxCollider bc in colliders) {
				bc.enabled = true;
			}
		}
		StopCoroutine ("ShieldColorTransition");
		StopCoroutine ("FadeTo");
		_shieldGlobe.gameObject.GetComponent < Renderer> ().material.color = new Color (1.0f,1.0f,1.0f,0.0f);
		_shieldGlobe.GetComponent<MeshRenderer> ().enabled = false;
	//	_shieldMaterial.color = new Color (1, 1, 1, 0.0f);
	//	Debug.Log ("StopHere");
	}

	IEnumerator ShieldColorTransition()
	{
		StartCoroutine (FadeTo(0.3f,1.0f,_shieldGlobe.transform));
		yield return new WaitForSeconds (1.0f);
		int counter = 0;
		while (counter<10) {
			StartCoroutine (FadeTo(0.6f,1.0f,_shieldGlobe.transform));
			yield return new WaitForSeconds (1.0f);
			counter++;
			StartCoroutine (FadeTo(0.3f,1.0f,_shieldGlobe.transform));
			yield return new WaitForSeconds (1.0f);
			counter++;
		}
		_shieldGlobe.gameObject.GetComponent < Renderer> ().material.color = new Color (1.0f,1.0f,1.0f,0.0f); 
	}

	IEnumerator FadeTo(float aValue, float aTime,Transform tx)
	{
		float alpha = tx.gameObject.GetComponent < Renderer>().material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			tx.gameObject.GetComponent < Renderer>().material.color = newColor;
			yield return null;
		}
	}
}