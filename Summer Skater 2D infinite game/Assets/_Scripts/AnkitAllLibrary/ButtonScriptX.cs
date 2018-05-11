using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonScriptX : MonoBehaviour {
	
	public UnityEvent _eventX,_eventY;

	public float differenceValue;

	public bool _playButtonSound;

	public AudioClip _buttonTapSound;

	private Vector3 _afterPress,_beforePress;

	// Use this for initialization
	void Start () {
		_beforePress = transform.localScale;
		_afterPress = new Vector3 (_beforePress.x-differenceValue,_beforePress.y-differenceValue,_beforePress.z);
	}

	void OnEnable()
	{
		_beforePress = transform.localScale;
		_afterPress = new Vector3 (_beforePress.x-differenceValue,_beforePress.y-differenceValue,_beforePress.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (_eventX != null) {
			_eventX.Invoke ();
			transform.localScale = _afterPress;
			if (_playButtonSound) {
				AnkitAudio._instance.PlaySfxWithForce (_buttonTapSound);
				Debug.Log ("Mouse Down");
			}
		}
	}

	void OnMouseUp()
	{
		if (_eventY != null) {
			_eventY.Invoke ();
			transform.localScale = _beforePress;
		}
	}
}