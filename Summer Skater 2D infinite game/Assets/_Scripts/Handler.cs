using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using AnkitTransformExtension;

public class Handler : MonoBehaviour {

	//-----------------------------------------------------Camera & Bases Initialisation----------------------------------------
	public static Handler _instance;
	public GameObject camObject;
	private Transform _boyMovableTransform;

	public GameObject[] BaseArray;
	public float BaseArraySize;
	public int BaseArrayIndicator=4;
	public float _speed=10.0f;

	public float comparePosition=40.0f;
	public float cameraPosition = 0.0f;

	public float nextBasePosition=100.0f;

	public List<GameObject> baseList;
	//---------------------------------------------------------------------------------------------------------------------------

	//-----------------------------------------------------Player Object Initialisation---------------------------------------------
	public GameObject playerObject;
	public Rigidbody playerObjectRb;

	//-----------------------------------------------------------------------------------------------------------------------------------------

	//--------------------------------------------------Tap Initialisation------------------------------------------------------------

	public int baseListIndicator=0;
	public int baseListRange=9;

	public int gameSpeed = 1;

	public ParticleSystem grassEmitter;

	public Animator _boyAnim;

	public GameObject[] _heartLife;

	//

	//-------------------------------------------------------------------------------------------------Cloud Pooling Initialisation--------------------------------------------------------

	public GameObject[] _cloudArray;
	public GameObject[] _midGroundArray1;
	public GameObject[] _midGroundArray2;

	public float _cloudComparePoint=1000.0f, _midGroundComparePoint=400.0f, _midGroundComparePoint2=700.0f;
	public float _cloudNextPoint=2376.0f, _midGroundNextPoint=1024.0f, _midGroundNextPoint2=1884.0f;
	public Vector3 _cloudNextPosition, _midGroundNextPosition,_midGroundNextPosition2;
	public int _cloudIndex = 0, _midGroundIndex = 0, _midGroundIndex2 = 0;

	//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

	public Transform _movableTransform;
	private Vector3 targetVector;

	private float _convertedSmoothPoint;

	public GameObject _gameOverScreen;

	public GameObject _rocketObject,_rocketObjectImage;
	public Vector3 _rocketObjectNextPosition;
	public bool _rocketUsed = false;

	public GameObject _scoreObject;
	public TextMesh _scoreMesh;
	public int _score=0;

	public int[] _highscoreArray;
	public TextMesh _finalScoreTextMesh;

	public GameObject _soundOnIcon, _soundOffIcon;
	public GameObject _musicOnIcon, _musicOffIcon;

	public bool _pauseScreen=false;

	public GameObject _pauseScreenObject;

	void OnEnable()
	{
		AnkitAudio.soundDelegates += SoundButtonAction;
		AnkitAudio.musicDelegates += MusicButtonAction;
	}

	void OnDisable()
	{
		AnkitAudio.soundDelegates -= SoundButtonAction;
		AnkitAudio.musicDelegates -= MusicButtonAction;
	}

	void Awake()
	{
		_instance = this;
		baseList = new List<GameObject> ();
		playerObjectRb = playerObject.GetComponent<Rigidbody> ();
		_movableTransform = camObject.gameObject.transform;
		_boyMovableTransform = playerObject.transform;
		_rocketObjectNextPosition = _rocketObject.transform.position;
	}

	void Start ()
	{
		BaseArraySize = BaseArray.Length - 1.0f;

		for (int i = 0; i < BaseArray.Length; i++) {
			baseList.Add (BaseArray[i].gameObject);
		}
		baseListRange = BaseArray.Length - 5;

		StartCoroutine ("SpeedCounter");

		for (int i = 0; i < _highscoreArray.Length; i++) {
			//_highscoreArray [i] = HomeManagement.highscoreArrayHome [i];
		}

		SoundButtonActionStart ();
		MusicButtonActionStart ();
	}

	void FixedUpdate () {
		
		_convertedSmoothPoint = _speed * gameSpeed;
		playerObjectRb.velocity = new Vector3 (_convertedSmoothPoint,playerObjectRb.velocity.y,playerObjectRb.velocity.z);

	}

	void Update()
	{
		//_boyMovableTransform.position = new Vector3(_boyMovableTransform.position.x+(10*Time.deltaTime),_boyMovableTransform.position.y,_boyMovableTransform.position.z);
		//_boyMovableTransform.position += Vector3.right * Time.deltaTime*10;
		//_boyMovableTransform.Translate (10.0f * Time.smoothDeltaTime, 0.0f, 0.0f);
		//_boyMovableTransform.Translate (Vector3.right*Time.fixedDeltaTime*10);
		//+= new Vector3 (10.0f*Time.smoothDeltaTime,0.0f,0.0f);
		cameraPosition = _movableTransform.position.x;
		//========================================================================== Base Pooling===================================================================
		if (cameraPosition>=comparePosition) {

			comparePosition += 20.0f;

			baseListIndicator = Random.Range (0, baseListRange);

			GameObject tempObject = baseList [baseListIndicator].gameObject;
			baseList.RemoveAt (baseListIndicator);
			baseList.Add (tempObject);

			tempObject.gameObject.transform.position = new Vector3 (nextBasePosition,-4.0f,0.0f);

			nextBasePosition += 20.0f;
			BaseArrayIndicator += 1;
		}
		//=======================================================================================================================================================
		_score = (int)_scoreObject.transform.position.x;
		_scoreMesh.text = "" + _score;
	}

	public void JumpFunction()
	{
		if (PlayerObject._instance.grounded) {
				_boyAnim.SetBool("Jump",true);
				playerObjectRb.velocity = new Vector3(playerObjectRb.velocity.x+1.0f,7.0f,playerObjectRb.velocity.z);
				StartCoroutine ("TapTime");
		}
	}

	public void DieFunction()
	{
		gameSpeed = 0;
		_boyAnim.SetBool ("Die",true);
		StartCoroutine ("GameOverScreenAnimation");
	}

	public IEnumerator GameOverScreenAnimation()
	{
		yield return new WaitForSeconds (1.0f);
		//------------------------------If Pause Screen On Then Close First-----------------------------------
		if (_pauseScreen) {
			_pauseScreenObject.SetActive (false);
			_pauseScreen = false;
			gameSpeed = 1;
		}
		//-----------------------------------------------------------------------------------------------------------------
		_gameOverScreen.SetActive (true);
		_finalScoreTextMesh.text = "" + _score;
		int tempInt;
		for (int i = 0; i < _highscoreArray.Length; i++) {
			if (_score >= _highscoreArray[i]) {
				tempInt = _highscoreArray [i];
				_highscoreArray [i] = _score;
				_score = tempInt;
			}	
		}
		for (int i = 0; i < _highscoreArray.Length; i++) {
			HomeManagement.highscoreArrayHome [i] = _highscoreArray [i];
		}
		PlayerPrefs.SetInt ("Highscore1",_highscoreArray[0]);
		PlayerPrefs.SetInt ("Highscore2",_highscoreArray[1]);
		PlayerPrefs.SetInt ("Highscore3",_highscoreArray[2]);
	}

	public IEnumerator TapTime()
	{
		yield return new WaitForSeconds (0.5f);
		_boyAnim.SetBool("Jump",false);
		yield return new WaitForSeconds (0.5f);
	}

	public IEnumerator SpeedCounter()
	{
		while (true) {
			yield return new WaitForSeconds (5.0f);
			_speed += 0.1f;
			if (RocketPowerScript._instance._boyPassed) {
				//int _rocketChoice = Random.Range (0,5);
				//if (_rocketChoice == 3) {
					RocketPowerScript._instance._boyPassed = false;
					_rocketObjectNextPosition.x = _boyMovableTransform.position.x + 50.0f;
					_rocketObject.transform.position = _rocketObjectNextPosition;
				//}
			}
		}
	}

	public void DownButtonPress()
	{
		_boyAnim.SetBool ("Down",true);
		playerObject.gameObject.transform.GetComponent<BoxCollider> ().center = new Vector3(0.0f,-1.0f,0.0f);
	}

	public void DownButtonRelease()
	{
		_boyAnim.SetBool ("Down",false);
		playerObject.gameObject.transform.GetComponent<BoxCollider> ().center = Vector3.zero;
	}

	public void LoadScene(string _sceneName)
	{
		SceneManager.LoadScene (_sceneName);
	}

	public void HighscoreWindowOpen()
	{
		HomeManagement._highscoreWindowOpen = true;
		SceneManager.LoadScene ("HomeScreen");
	}

	public void SoundButtonAction()
	{
		if (AnkitAudio._soundStatus) {
			_soundOnIcon.SetActive (false);
			_soundOffIcon.SetActive (true);
		} else {
			_soundOffIcon.SetActive (false);
			_soundOnIcon.SetActive (true);
		}
	}

	public void SoundButtonActionStart()
	{
		if (!AnkitAudio._soundStatus) {
			_soundOnIcon.SetActive (false);
			_soundOffIcon.SetActive (true);
		} else {
			_soundOffIcon.SetActive (false);
			_soundOnIcon.SetActive (true);
		}
	}

	public void MusicButtonAction()
	{
		if (AnkitAudio._musicStatus) {
			_musicOnIcon.SetActive (false);
			_musicOffIcon.SetActive (true);
		} else {
			_musicOffIcon.SetActive (false);
			_musicOnIcon.SetActive (true);
		}
	}

	public void MusicButtonActionStart()
	{
		if (!AnkitAudio._musicStatus) {
			_musicOnIcon.SetActive (false);
			_musicOffIcon.SetActive (true);
		} else {
			_musicOffIcon.SetActive (false);
			_musicOnIcon.SetActive (true);
		}
	}

	public void MusicButtonCall()
	{
		AnkitAudio._instance.MusicButtonCall ();
	}

	public void SoundButtonCall()
	{
		AnkitAudio._instance.SoundButtonCall ();
	}

	public void PauseScreenCall()
	{
		if (_pauseScreen) {
			_pauseScreenObject.SetActive (false);
			_pauseScreen = false;
			gameSpeed = 1;
		} else {
			_pauseScreenObject.SetActive (true);
			_pauseScreen = true;
			gameSpeed = 0;
		}
	}
}