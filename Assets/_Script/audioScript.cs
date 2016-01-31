using UnityEngine;
using System.Collections.Generic;

public class audioScript : MonoBehaviour
{
	public List<AudioClip> lstSound;
	public AudioSource soundsSource;
	public AudioSource soundsSourceLoop;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	internal void play(string id, bool isLoop)
	{
		AudioClip curClip = lstSound.Find(X => X.name == id);

		if (curClip == null)
		{ 
			Debug.Log("Sound not found " + id);
			return;
		}

		if (isLoop)
		{
			soundsSourceLoop.clip = curClip;
			soundsSourceLoop.Play();
		
			Debug.Log("Play sound loop " + id);
		}
		else
			soundsSource.PlayOneShot(curClip);
		//if (audioSrc == null)
		//{
		//	Debug.Log("No audiosrc");
		//	return;
		//}
		//audioSrc[0].PlayOneShot()
		//if(id < audioSrc.Length)
		//{
		//	audioSrc[id].Play();
		//}
	}
}
