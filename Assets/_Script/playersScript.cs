using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;
using System;

class playersScript
{
	public playerScript[] lstPlayer = new playerScript[Gvar.nbPlayerMax];

	public playersScript()
	{

	}

	internal bool exists(int numPlayer)
	{
		return lstPlayer[numPlayer] != null;
	}

	internal void setReady(int numPlayer)
	{
		lstPlayer[numPlayer].ready = true;
	}

	internal msgResponse treatMessage(int numPlayer, string curCmd)
	{
		return lstPlayer[numPlayer].treatMessage(curCmd);
	}

	internal bool allReady()
	{
		Debug.Log("Check ready");
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null && lstPlayer[i].ready == false)
			{
				return false;
			}
		}
		return true;
	}

	internal void reset()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].score = 0;
				lstPlayer[i].ready = false;
			}
		}
	}

	internal void delete(logicScript logicScript, int device_id)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

		if (lstPlayer[numPlayer] != null)
		{
			logicScript.Destroy(lstPlayer[numPlayer].gameObject);
			lstPlayer[numPlayer] = null;
		}
	}

	internal List<playerScript> getList()
	{
		List<playerScript> lstP = new List<playerScript>();

		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstP.Add(lstPlayer[i]);
			}
		}
		return lstP;
	}

	internal playerScript add(logicScript logicScript, int device_id)
	{
		GameObject newPlayer = logicScript.Instantiate(logicScript.prefabPlayer);
		playerScript newScript = newPlayer.GetComponent<playerScript>();
		newScript.init(device_id);
		lstPlayer[newScript.player] = newScript;

		return newScript;
	}

	internal void sendRoles()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].setRole();
			}
		}

	}
}

