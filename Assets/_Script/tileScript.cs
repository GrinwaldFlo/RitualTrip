using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour
{
	public GameObject goSpaceStation;
	public GameObject goDungeon;
	public GameObject goForest;
	public GameObject goSupermarket;
	public GameObject goTrain;
	public GameObject goAcropolis;
	public GameObject goCervin;

	private void hideAll()
	{
		goSpaceStation.SetActive(false);
		goDungeon.SetActive(false);
		goForest.SetActive(false);
		goSupermarket.SetActive(false);
		goTrain.SetActive(false);
		goAcropolis.SetActive(false);
		goCervin.SetActive(false);
	}

	internal void show(clPlace place)
	{
		hideAll();
		switch (place.textEN)
		{
			case "a good old dungeon":
				goDungeon.SetActive(true);
				break;
			case "a space station":
				goSpaceStation.SetActive(true);
				break;
			case "an enchanted forest":
				goForest.SetActive(true);
				break;
			case "a supermarket":
				goSupermarket.SetActive(true);
				break;
			case "a train":
				goTrain.SetActive(true);
				break;
			case "an acropolis":
				goAcropolis.SetActive(true);
				break;
			case "the Matterhorn":
				goCervin.SetActive(true);
				break;
			default:
				break;
		}
	}
}
