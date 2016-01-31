using UnityEngine;
using System.Collections;

public class tileScript : MonoBehaviour
{
	public GameObject goSpaceStation;
	public GameObject goDungeon;
	public GameObject goForest;
	public GameObject goSupermarket;
	public GameObject goTrain;

	private void hideAll()
	{
		goSpaceStation.SetActive(false);
		goDungeon.SetActive(false);
		goForest.SetActive(false);
		goSupermarket.SetActive(false);
		goTrain.SetActive(false);
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
			default:
				break;
		}
	}
}
