using UnityEngine;
using System.Collections;

public class objectScript : MonoBehaviour
{
	public GameObject goDeadBody;
	public GameObject goAltar;
	public GameObject goRubberChicken;
	public GameObject goLightSaber;
	public GameObject goChickenSandwich;
	public GameObject goMobilePhone;
	public GameObject goFlower;
	public GameObject goHamster;
	public GameObject goChandelier;

	private void hideAll()
	{
		goDeadBody.SetActive(false);
		goAltar.SetActive(false);
		goRubberChicken.SetActive(false);
		goLightSaber.SetActive(false);
		goChickenSandwich.SetActive(false);
		goMobilePhone.SetActive(false);
		goFlower.SetActive(false);
		goHamster.SetActive(false);
		goChandelier.SetActive(false);
	}

	internal void show(clObject obj)
	{
		hideAll();
		switch (obj.id)
		{
			case "deadBody":
				goDeadBody.SetActive(true);
				break;
			case "altar":
				goAltar.SetActive(true);
				break;
			case "rubberChicken":
				goRubberChicken.SetActive(true);
				break;
			case "lightSaber":
				goLightSaber.SetActive(true);
				break;
			case "chickenSandwich":
				goChickenSandwich.SetActive(true);
				break;
			case "mobilePhone":
				goMobilePhone.SetActive(true);
				break;
			case "flower":
				goFlower.SetActive(true);
				break;
			case "hamster":
				goHamster.SetActive(true);
				break;
			case "chandelier":
				goChandelier.SetActive(true);
				break;
			default:
				break;
		}
	}

}
