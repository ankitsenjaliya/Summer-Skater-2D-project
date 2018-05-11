using UnityEngine;
using System.Collections;

public class PlayerFollowCamera : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

	private Transform playerTransform;


	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	private Vector3 targetPosition;

	private float timer = 0.0f;

	public float smoothSpeed = 15;

	public bool smooth;



	// Use this for initialization
	void Start () 
	{
		playerTransform = player.transform;
		targetPosition = transform.position;
	}

	void LateUpdate()
	{
		targetPosition.x = player.transform.position.x;
		transform.position = targetPosition;
	}
}