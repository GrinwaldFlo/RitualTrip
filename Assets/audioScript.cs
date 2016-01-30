using UnityEngine;
using System.Collections;

public class audioScript : MonoBehaviour
{
	public GameObject[] sounds;

	private AudioSource[] audioSrc;

	// Use this for initialization
	void Start()
	{
		audioSrc = new AudioSource[sounds.Length];

		for (int i = 0; i < sounds.Length; i++)
		{
			audioSrc[i] = sounds[i].GetComponent<AudioSource>();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	internal void play(int id)
	{
		if(audioSrc.Length < id)
		{
			audioSrc[id].Play();
		}
	}
}
