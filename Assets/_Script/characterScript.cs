using UnityEngine;
using System.Collections;

public class characterScript : MonoBehaviour
{
	public GameObject goPaladin;
	public GameObject goPlumber;
	public GameObject goTrekker;
	public GameObject goAccountant;
	public GameObject goMurderer;
	public GameObject goScreenwriter;
	public GameObject goNurse;
	public GameObject goNecromancer;
	public GameObject goSoldier;
	public GameObject goUnicorn;
	public GameObject goCyborg;
	public GameObject goPrincess;
	public GameObject goSayen;

	private void hideAll()
	{
		goPaladin.SetActive(false);
		goPlumber.SetActive(false);
		goTrekker.SetActive(false);
		goAccountant.SetActive(false);
		goMurderer.SetActive(false);
		goScreenwriter.SetActive(false);
		goNurse.SetActive(false);
		goNecromancer.SetActive(false);
		goSoldier.SetActive(false);
		goUnicorn.SetActive(false);
		goCyborg.SetActive(false);
		goPrincess.SetActive(false);
		goSayen.SetActive(false);
	}

	internal void show(clHero pnj)
	{
		hideAll();
		switch (pnj.getLabel())
		{
			case "paladin":
				goPaladin.SetActive(true);
				break;
			case "plumber":
				goPlumber.SetActive(true);
				break;
			case "trekker":
				goTrekker.SetActive(true);
				break;
			case "accountant":
				goAccountant.SetActive(true);
				break;
			case "murderer":
				goMurderer.SetActive(true);
				break;
			case "screenwriter":
				goScreenwriter.SetActive(true);
				break;
			case "flower":
				goNurse.SetActive(true);
				break;
			case "necromancer":
				goNecromancer.SetActive(true);
				break;
			case "soldier":
				goSoldier.SetActive(true);
				break;
			case "unicorn":
				goUnicorn.SetActive(true);
				break;
			case "Cyborg":
				goCyborg.SetActive(true);
				break;
			case "Princess":
				goPrincess.SetActive(true);
				break;
			case "Sayen":
				goSayen.SetActive(true);
				break;
			default:
				break;
		}
	}
}
