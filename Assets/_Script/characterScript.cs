using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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


	public List<GameObject> lstObstacle = new List<GameObject>();

	public GameObject placeHolder;
	public float repulsionVisitor = -1f;
	public float repulsionObstacle = -1f;
	public float repulsionDistance = 1f;
	public float attractionAttraction = 5f;
	public float vAng = 0.1f;
	public float vLin = 0.1f;
	public float angleDif;
	public float posReached = 0.5f;
	public float finalScale = 1.5f;
	public float vScale = 0.1f;

	private Animator an;
	private Vector3 tmpVector;
	public Vector3 vResult;
	private Vector3 cross;


	public void Start()
	{
		an = GetComponent<Animator>();
	}

	private bool lastWalk;
	public void walk(bool value)
	{
		if (value == lastWalk)
			return;
		lastWalk = value;
		an.SetBool("Walk", value);
	}

	/// <summary>
	/// Adds the force.
	/// </summary>
	/// <param name="factor">Positive = obstacle, negative = honypot</param>
	/// <param name="item">Item.</param>
	void addForce(float factor, Transform item)
	{
		tmpVector = new Vector3(transform.position.x - item.position.x, 0f, transform.position.z - item.position.z);

	//	tmpVector.Normalize();
		tmpVector = tmpVector * factor;

		vResult.x -= tmpVector.x;
		vResult.z -= tmpVector.z;
	}

	void FixedUpdate()
	{
		if(transform.localScale.x < finalScale)
		{
			transform.localScale = new Vector3(transform.localScale.x + vScale, transform.localScale.x + vScale, transform.localScale.x + vScale);
		}

		vResult = new Vector3();
		//forceWall();
		forceObstacle();
		if (placeHolder != null)
		{
			addForce(attractionAttraction, placeHolder.transform);
		}
		//	forceBot();

		if(vResult.magnitude < posReached)
		{
			walk(false);

			setAngle(placeHolder.transform.forward, false);
			return;
		}
		walk(true);

		setAngle(vResult, true);
	}

	void setAngle(Vector3 vFinal, bool translate)
	{
		angleDif = Vector3.Angle(vFinal, transform.forward);

		cross = Vector3.Cross(vFinal, transform.forward);
		
		if (translate && angleDif < 20f)
			transform.Translate(vFinal.normalized * vLin, Space.World);

		if (angleDif > 1f)
		{
			if (cross.y > 0f)
				transform.Rotate(0, -vAng, 0);
			else
				transform.Rotate(0, vAng, 0);
		}
	}

	void forceObstacle()
	{
		foreach (GameObject item in lstObstacle)
		{
			if(Vector3.Distance(this.transform.position, item.transform.position) < repulsionDistance)
				addForce(repulsionObstacle, item.transform);
		}
	}

	//void forceBot()
	//{
	//	int i = 0;
	//	while (i < lstVisitor.Count)
	//	{
	//		if (lstVisitor[i] != null)
	//		{
	//			addForce(repulsionVisitor, lstVisitor[i].transform, 2f);
	//			i++;
	//		}
	//		else
	//		{
	//			lstVisitor.RemoveAt(i);
	//		}
	//	}
	//}

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
			case "necromancer":
				goNecromancer.SetActive(true);
				break;
			case "soldier":
				goSoldier.SetActive(true);
				break;
			case "unicorn":
				goUnicorn.SetActive(true);
				break;
			case "cyborg":
				goCyborg.SetActive(true);
				break;
			case "princess":
				goPrincess.SetActive(true);
				break;
			case "Sayen":
				goSayen.SetActive(true);
				break;
			case "nurse":
				goNurse.SetActive(true);
				break;
			default:
				break;
		}
	}
}
