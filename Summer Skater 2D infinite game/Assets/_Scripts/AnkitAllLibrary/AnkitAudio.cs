using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AnkitAudio : MonoBehaviour {

	public static AnkitAudio _instance;
	public AudioSource BG,sfxAudioSource;

	public static bool _soundStatus = true;
	public delegate void SoundDelegate();
	public static event SoundDelegate soundDelegates;

	public static bool _musicStatus = true;
	public delegate void MusicDelegate();
	public static event MusicDelegate musicDelegates;

	void Awake()
	{
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this);
		} else {
			DestroyImmediate(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		_soundStatus = true;
		_musicStatus = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySfxWithoutForce (AudioClip _audioClip)
	{
		sfxAudioSource.PlayOneShot (_audioClip);
	}

	public void PlaySfxWithForce(AudioClip _audioClip)
	{
		if (sfxAudioSource.isPlaying) {
			sfxAudioSource.Stop ();
			sfxAudioSource.PlayOneShot (_audioClip);
		} else {
			sfxAudioSource.PlayOneShot (_audioClip);
		}
	}

	public void StopAllSound()
	{
		sfxAudioSource.Stop ();
		BG.Stop ();
	}

	public void SoundButtonCall()
	{
		if (_soundStatus) {
			if (soundDelegates != null) {
				soundDelegates ();
			}
			_soundStatus = false;
			sfxAudioSource.volume = 0.0f;
		} else {
			if (soundDelegates != null) {
				soundDelegates ();
			}
			_soundStatus = true;
			sfxAudioSource.volume = 1.0f;
		}
	}

	public void MusicButtonCall()
	{
		if (_musicStatus) {
			if (musicDelegates != null) {
				musicDelegates ();
			}
			_musicStatus = false;
			BG.volume = 0.0f;
		} else {
			if (musicDelegates != null) {
				musicDelegates ();
			}
			_musicStatus = true;
			BG.volume = 1.0f;
		}
	}
}
