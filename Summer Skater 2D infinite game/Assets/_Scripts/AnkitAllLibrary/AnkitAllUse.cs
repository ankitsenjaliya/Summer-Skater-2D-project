using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AnkitTransformExtension
{
	public class AnkitAllUse:MonoBehaviour {

		public static AnkitAllUse _instance;

		public List<Coroutine> inst;

		public List<string> storedTransformName;

		public string newTransformName;

		void Awake()
		{
			_instance = this;
			storedTransformName = new List<string> ();
			inst=new List<Coroutine>();
		}

		public void DoPosition(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve,string animationMode)
		{
			newTransformName = x.name;
		//	if(!storedTransformName.Contains(newTransformName))
		//	{
				storedTransformName.Add(newTransformName);
		//	}
		//	int indexNo = storedTransformName.IndexOf (newTransformName);
		//	Debug.Log ("indexNo"+indexNo);
			if (animationMode == "GP") {
				inst.Add(StartCoroutine (DoGlobalPositionCoroutine(x,startVector,targetVector,startTime,durationTime,curve)));
			} else if (animationMode == "LP") {
				inst.Add(StartCoroutine (DoLocalPositionCoroutine(x,startVector,targetVector,startTime,durationTime,curve)));
			}
			else if (animationMode == "GE") {
				inst.Add(StartCoroutine (DoGlobalEulerCoroutine(x,startVector,targetVector,startTime,durationTime,curve)));
			}
			else if (animationMode == "LE") {
				inst.Add(StartCoroutine (DoLocalEulerCoroutine(x,startVector,targetVector,startTime,durationTime,curve)));
			}
			/*else if (animationMode == "GS") {
				inst.Add(StartCoroutine (DoGlobalScaleCoroutine(x,targetVector,startTime,durationTime,curve)));
			}*/
			else if (animationMode == "LS") {
				inst.Add(StartCoroutine (DoLocalScaleCoroutine(x,startVector,targetVector,startTime,durationTime,curve)));
			}
		}

		public void StopAnimation(Transform x)
		{
			int sameType = 0;
			string newTransformName = x.name;
			for (int i = 0; i < storedTransformName.Count; i++) {
				if (newTransformName==storedTransformName[i]) {
					sameType++;
				}
			}

			for (int i = 0; i < sameType; i++) {
				int indexNo = storedTransformName.IndexOf (newTransformName);
				storedTransformName.RemoveAt(indexNo);
				StopCoroutine (inst[indexNo]);
				inst.RemoveAt (indexNo);
			}
		}
		
		public IEnumerator DoGlobalPositionCoroutine(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			//Vector3 startVector = x.position;
			while(timer<=durationTime)
			{
				x.position=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.deltaTime;
				yield return null;
			}
		}

		public IEnumerator DoLocalPositionCoroutine(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			//Vector3 startVector = x.localPosition;
			while(timer<=durationTime)
			{
				x.localPosition=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.smoothDeltaTime;
				yield return null;
			}
		}

		public IEnumerator DoGlobalEulerCoroutine(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			//Vector3 startVector = x.eulerAngles;
			while(timer<=durationTime)
			{
				x.eulerAngles=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.deltaTime;
				yield return null;
			}
		}

		public IEnumerator DoLocalEulerCoroutine(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			//Vector3 startVector = x.localEulerAngles;
			while(timer<=durationTime)
			{
				x.localEulerAngles=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.deltaTime;
				yield return null;
			}
		}

		/*public IEnumerator DoGlobalScaleCoroutine(Transform x,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			Vector3 startVector = x.lossyScale;
			while(timer<=durationTime)
			{
				x.lossyScale=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.deltaTime;
				yield return null;
			}
		}*/

		public IEnumerator DoLocalScaleCoroutine(Transform x,Vector3 startVector,Vector3 targetVector,float startTime,float durationTime,AnimationCurve curve)
		{
			yield return new WaitForSeconds (startTime);
			float timer = 0.0f;
			//Vector3 startVector = x.localScale;
			while(timer<=durationTime)
			{
				x.localScale=Vector3.Lerp(startVector,targetVector,curve.Evaluate(timer/durationTime));
				timer+=Time.deltaTime;
				yield return null;
			}
		}
	}
}