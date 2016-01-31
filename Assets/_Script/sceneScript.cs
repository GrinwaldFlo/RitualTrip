using UnityEngine;
using System.Collections;

public class sceneScript : MonoBehaviour
{
	public GameObject goObj;
	public GameObject goTile;
	public GameObject goPnj;

	private objectScript scrObj;
	private tileScript scrTile;
	private characterScript scrPnj;

	// Use this for initialization
	void Start()
	{
		scrObj = goObj.GetComponent<objectScript>();
		scrTile = goTile.GetComponent<tileScript>();
		scrPnj = goPnj.GetComponent<characterScript>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	internal void showObj(clObject obj)
	{
		scrObj.show(obj);
	}

	internal void showTile(clPlace place)
	{
		scrTile.show(place);
	}

	internal void showPng(clHero pnj)
	{
		scrPnj.show(pnj);
	}
}
