using UnityEngine;
using System.Collections;
using AnkitTransformExtension;

public class PlayerObject : MonoBehaviour {

	public static PlayerObject _instance;

	public GameObject[] _heartLife;
	public int _lifeNo=2;

	public bool grounded = true;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;

	public string _objCurrName,_objPrevName;
	public string _objCurrName2,_objPrevName2;
	public ParticleSystem _obstacleParticle;

	public Transform camTransform;
	public float shakeDuration = 0.0f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	Vector3 originalPos;

	//-----------------------------Rocker Effect Initialisation------------------------------------
	public GameObject _rocketShield1,_rocketShield2,_rocketWithSkateBoard;
	public ParticleSystem _speedSonicFlares, _rocketExhaustFlaresParticle;
	//-----------------------------------------------------------------------------------------------------

	public Animator _boyAnim;

	public GameObject _rocketObjectPower;

	public int _score,_tempScore1,_tempScore2;

	public GameObject[] _obstaclesArray;

	public GameObject _rocketProgressBarObject,_rocketProgressBar,_rocketProgressBarEmpty;
	Vector3 _rocketProgressBarFull;

	public AnimationCurve _curveX;

	void Awake()
	{
		_instance = this;
	}

	void Start()
	{
		for (int i = 0; i < 3; i++) {
			_heartLife [i].SetActive (true);
		}
		_objCurrName = "Obstacleq";
		_objPrevName = "Obstaclew";

		_objCurrName2 = "Obstaclex";
		_objPrevName2 = "Obstacley";

		originalPos = camTransform.localPosition;

		_rocketProgressBarFull = _rocketProgressBar.transform.localScale;
	}

	void FixedUpdate()
	{
		grounded = Physics.CheckSphere (groundCheck.position,groundCheckRadius,groundLayer);
	}

	void OnCollisionEnter(Collision col)
	{
		_objCurrName = col.gameObject.name;
		if (col.gameObject.CompareTag ("Obstacle") && (_objCurrName !=_objPrevName)) {
			Debug.Log ("Here Enter");
			StartCoroutine (PlayParticle(_obstacleParticle,1.0f));
			StartCoroutine ("CameraShake");

			if (_lifeNo > -1) {
				_heartLife [_lifeNo].SetActive (false);
				//-------------------Obstacle apear and disapear------------
				if (_lifeNo > 0) {
					GameObject temp = col.gameObject;
					StartCoroutine (ObjectReset(temp));
				}
				//-------------------Gameover counting------------------------
				if (_lifeNo==0) {
					//Debug.Log ("Die Animation should play");
					Handler._instance.DieFunction ();
				}
				_lifeNo = _lifeNo - 1;
			}

			_objPrevName = col.gameObject.name;
		}
	}

	void OnTriggerEnter(Collider colt)
	{
		_objCurrName2 = colt.gameObject.name;
		if (colt.gameObject.CompareTag ("Rocket") && (_objCurrName2 != _objPrevName2)) {
			_rocketObjectPower = colt.gameObject;
			colt.gameObject.SetActive (false);
			StartCoroutine ("RocketTravelling");
			_objPrevName2 = colt.gameObject.name;
		}
		if (colt.gameObject.CompareTag ("RocketPowerChecker") && (_objCurrName2 != _objPrevName2)) {
			_objPrevName2 = colt.gameObject.name;
		}
	}

	public IEnumerator RocketTravelling()
	{
		StartCoroutine ("RocketProgressBar");
		_rocketShield1.SetActive (true);
		_rocketShield2.SetActive (true);
		_rocketWithSkateBoard.SetActive (true);
		_rocketExhaustFlaresParticle.Play ();
		_speedSonicFlares.Play ();
		_boyAnim.SetBool ("Rocket",true);
		Handler._instance._speed += 10.0f;
		for (int i = 0; i < _obstaclesArray.Length; i++) {
			_obstaclesArray [i].GetComponent<BoxCollider> ().enabled = false;
		}
		yield return new WaitForSeconds (10.0f);
		for (int i = 0; i < _obstaclesArray.Length; i++) {
			_obstaclesArray [i].GetComponent<BoxCollider> ().enabled = true;
		}
		_rocketShield1.SetActive (false);
		_rocketShield2.SetActive (false);
		_rocketWithSkateBoard.SetActive (false);
		_rocketExhaustFlaresParticle.Stop ();
		_speedSonicFlares.Stop ();
		_boyAnim.SetBool ("Rocket",false);
		Handler._instance._speed -= 10.0f;
		_rocketObjectPower.SetActive (true);
	}

	public IEnumerator RocketProgressBar()
	{
		_rocketProgressBarObject.SetActive (true);
		_rocketProgressBar.transform.localScale = _rocketProgressBarFull;
		AnkitAllUse._instance.DoPosition (_rocketProgressBar.transform,_rocketProgressBar.transform.localScale,_rocketProgressBarEmpty.transform.localScale,0.0f,10.0f,_curveX,"LS");
		yield return new WaitForSeconds (10.0f);
		_rocketProgressBarObject.SetActive (false);
	}

	IEnumerator PlayParticle(ParticleSystem _particle,float duration)
	{
		_particle.Play ();
		yield return new WaitForSeconds (duration);
		_particle.Stop ();
	}

	public IEnumerator ObjectReset(GameObject ob)
	{
		ob.SetActive (false);
		yield return new WaitForSeconds (2.0f);
		ob.SetActive (true);

	}

	IEnumerator CameraShake()
	{
		while (true) {
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			if (shakeDuration <= 0) {
				camTransform.localPosition = originalPos;
				shakeDuration = 0.2f;
				yield break;
			}
			yield return null;
		}
	}
}