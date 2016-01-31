using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

class playersScript
{
	public playerScript[] lstPlayer = new playerScript[Gvar.nbPlayerMax];
	internal static List<clHero> lstHeros;
	internal static List<clEmotion> lstEmotion;

	public playersScript()
	{

	}

	internal static void init()
	{
		lstHeros = new List<clHero>();
		lstHeros.AddRange(Gvar.lstHeros);

		lstEmotion = new List<clEmotion>();
		lstEmotion.AddRange(Gvar.lstEmotion);
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
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null && lstPlayer[i].ready == false)
			{
				return false;
			}
		}
		return true;
	}

	internal bool allAnswered()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null && lstPlayer[i].answer == -1)
			{
				return false;
			}
		}
		return true;
	}

	internal void resetReady()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].ready = false;
			}
		}
	}

	internal void resetScene()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].answer = -1;
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

	internal List<clAnswer> getResult()
	{
		int[] r = new int[3];
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				r[lstPlayer[i].answer]++;
			}
		}
		List<clAnswer> lstR = new List<clAnswer>();
		for (int i = 0; i < r.Length; i++)
		{
			lstR.Add(new clAnswer(i, r[i]));
		}
		return lstR;
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

	internal clEmotion updWinLose(List<clAnswer> lstF)
	{
		List<clAnswer> lstAnswerWin = lstF.FindAll(X => X.score == lstF[0].score);
		clEmotion[] lstEmotionWin = new clEmotion[lstAnswerWin.Count];

		for (int i = 0; i < lstAnswerWin.Count; i++)
		{
			lstEmotionWin[i] = Gvar.lstEmotion.Find(X => X.id == lstAnswerWin[i].id);
		}

		

		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].win = false;
				if (lstPlayer[i].emotion.id == lstEmotionWin[0].id)
				{
					lstPlayer[i].score = lstPlayer[i].score + 1; 
					lstPlayer[i].win = true;
				}
			}
		}

		return lstEmotionWin[0];
	}

	internal void sendWinLoose(clEmotion emotionWin)
	{
		string txt = Cmd.End + emotionWin.getWin() + "<br><b>";

		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].sendWinLoose(txt);
			}
		}
	}

	internal void playSound(clSound curSound)
	{
		List<playerScript> lst = getList();

		lst[Random.Range(0, lst.Count)].playSound(curSound);
	}
}

