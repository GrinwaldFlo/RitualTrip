using UnityEngine;
using System.Collections;

public class sceneScript : MonoBehaviour
{
	public GameObject goObj;
	public GameObject goTile;
	public GameObject goPnj;

	public GameObject goPop;
	public GameObject goEnd;
	public GameObject[] goHeroPos;
	public GameObject prefabCharacter;
	public GameObject goStraw;

	private objectScript scrObj;
	private tileScript scrTile;
	private characterScript scrPnj;

	// Use this for initialization
	void Start()
	{
		scrObj = goObj.GetComponent<objectScript>();
		scrTile = goTile.GetComponent<tileScript>();
		scrPnj = goPnj.GetComponent<characterScript>();

		showStraw(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	internal void showStraw(bool val)
	{
		goObj.SetActive(!val);
		goTile.SetActive(!val);
		goPnj.SetActive(!val);
		goStraw.SetActive(val);
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

	internal void popPlayer(playerScript player)
	{

	}
}
