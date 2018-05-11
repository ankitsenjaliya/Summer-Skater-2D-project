using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeManagement : MonoBehaviour {

	public GameObject _objRoot;

	public GameObject _highscoreWindowCam;
	public GameObject _highscoreWindow,_highSWTopposition,_highSWDownposition;

	public GameObject _soundOnIcon, _soundOffIcon;
	public GameObject _musicOnIcon, _musicOffIcon;

	public AnimationCurve _curveX;

	public static int[] highscoreArrayHome;
	public TextMesh[] _highscoreTextMeshHome;

	public GameObject _highscoreCollider;

	public static bool _highscoreWindowOpen;

	public AudioClip[] _sfxClips;

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

	// Use this for initialization
	void Start () {
		highscoreArrayHome = new int[3];
		if (!PlayerPrefs.HasKey("Highscore1")) {
			highscoreArrayHome [0] = 0;
			PlayerPrefs.SetInt ("Highscore1",highscoreArrayHome[0]);
		}
		if (!PlayerPrefs.HasKey("Highscore2")) {
			highscoreArrayHome [1] = 0;
			PlayerPrefs.SetInt ("Highscore2",highscoreArrayHome[1]);
		}
		if (!PlayerPrefs.HasKey("Highscore3")) {
			highscoreArrayHome [2] = 0;
			PlayerPrefs.SetInt ("Highscore3",highscoreArrayHome[2]);
		}
		highscoreArrayHome [0] = PlayerPrefs.GetInt ("Highscore1");
		highscoreArrayHome [1] = PlayerPrefs.GetInt ("Highscore2");
		highscoreArrayHome [2] = PlayerPrefs.GetInt ("Highscore3");

		for (int i = 0; i < _highscoreTextMeshHome.Length; i++) {
			_highscoreTextMeshHome[i].text = "" + highscoreArrayHome[i];
		}

		if (_highscoreWindowOpen) {
			OpenHighScoreWindow ();
		}
		_highscoreWindowOpen = false;
		SoundButtonActionStart ();
		MusicButtonActionStart ();
	}
		
	// Update is called once per frame
	void Update () {
		_objRoot.transform.localEulerAngles += new Vector3 (0.0f,10*Time.smoothDeltaTime,0.0f);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void OpenHighScoreWindow()
	{
		_highscoreCollider.GetComponent<BoxCollider> ().enabled = false;
		_highscoreWindowCam.SetActive (true);
		_highscoreWindow.transform.localPosition = _highSWDownposition.transform.localPosition;
	}

	public void CloseHighScoreWindow()
	{
		_highscoreCollider.GetComponent<BoxCollider> ().enabled = true;
		_highscoreWindow.transform.localPosition = _highSWTopposition.transform.localPosition;
		_highscoreWindowCam.SetActive (false);
	}

	IEnumerator CloseCam()
	{
		yield return new WaitForSeconds (0.4f);
		_highscoreWindowCam.SetActive (false);
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
}