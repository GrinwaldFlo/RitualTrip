using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class logicScript : MonoBehaviour
{
	public GameObject introCanvas;
	//public GameObject intro2Canvas;
	public GameObject playCanvas;
	public GameObject resultCanvas;
	public GameObject endCanvas;
	public GameObject uiPlayLeft;
	public GameObject uiPlayRight;
	public GameObject prefabPlayer;

	public GameObject sounds;

	public Text txtMessageIntro;
	public Text txtMessagePlay;
	public Text txtMessageEnd;

	private float timeStateChange;

	private playersScript players;

	public delegate void StateChangeAction();
	public static event StateChangeAction OnStateChange;


	void Awake()
	{
		AirConsole.instance.onConnect += Instance_onConnect;
		AirConsole.instance.onCustomDeviceStateChange += Instance_onCustomDeviceStateChange;
		AirConsole.instance.onDeviceStateChange += Instance_onDeviceStateChange;
		AirConsole.instance.onDisconnect += Instance_onDisconnect;
		AirConsole.instance.onMessage += Instance_onMessage;
		AirConsole.instance.onReady += Instance_onReady;
		init();
	}

	private void Instance_onReady(string code)
	{
		Debug.Log("Ready " + code);
	}

	private void init()
	{
		Gvar.init();
		players = new playersScript();
		setGameState(enGameState.Intro);
	}

	private void setGameState(enGameState state)
	{
		if (Gvar.gameState != state)
		{
			Gvar.setGameState(state);
			timeStateChange = Time.time;
			if (OnStateChange != null)
				OnStateChange();

			hideAllCanvas();
			switch (state)
			{
				case enGameState.Intro:
					introCanvas.SetActive(true);
					if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 0)
					{
						AirConsole.instance.Broadcast(Cmd.Intro + Lng.Intro);
					}
					break;
				case enGameState.Intro2:
					//intro2Canvas.SetActive(true);
					players.sendRoles();
					break;
				case enGameState.Play:
					playCanvas.SetActive(true);

					setNewScene();
					break;
				case enGameState.Result:
					resultCanvas.SetActive(true);
					break;
				case enGameState.End:
					endCanvas.SetActive(true);
					if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 0)
					{
						AirConsole.instance.Broadcast(Cmd.End);
					}

					break;
				default:
					break;
			}
		}
	}

	private void setNewScene()
	{
		clHero pnj = Gvar.lstHeros[Random.Range(0, Gvar.lstHeros.Count)];
		clEmotion pnjEmotion = Gvar.lstEmotion[Random.Range(0, Gvar.lstEmotion.Count)];
		clObject obj = Gvar.lstObject[Random.Range(0, Gvar.lstObject.Count)];
		clPlace place = Gvar.lstPlace[Random.Range(0, Gvar.lstPlace.Count)];
		int emotionSide = Random.Range(0, 2);

		clAction action = Gvar.lstAction.Find(x => x.emotion == pnjEmotion.getText(emotionSide));

		if (action == null)
		{ 
			Debug.Log("Emotion not found in action " + pnjEmotion.getText(emotionSide));
			AirConsole.instance.Broadcast(Cmd.Play + "Emotion not found in action " + pnjEmotion.getText(emotionSide));
			return;
		}

		
		string actionSentence = string.Format(action.sentence, obj.getLabel());
		string r = string.Format("In {0}, {1} {2}.", place.textEN, Gvar.addDet(pnj.getLabel()), actionSentence);

		txtMessagePlay.text = place.textEN + "\r\n" + place.descEN;
		AirConsole.instance.Broadcast(Cmd.Play + r);
		AirConsole.instance.Broadcast(Cmd.But + "0|" + action.help);
		AirConsole.instance.Broadcast(Cmd.But + "1|" + action.block);
		AirConsole.instance.Broadcast(Cmd.But + "2|" + action.doNothing);
		//AirConsole.instance.Broadcast(Cmd.But + "3|"+ Lng.Answer4);
	}

	private void hideAllCanvas()
	{
		introCanvas.SetActive(false);
		//intro2Canvas.SetActive(false);
		playCanvas.SetActive(false);
		resultCanvas.SetActive(false);
		endCanvas.SetActive(false);
	}

	private void Instance_onMessage(int from, JToken data)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);

		if (!players.exists(numPlayer))
		{
			Debug.Log("Player " + numPlayer + " doesn't exists");
			return;
		}
		string curCmd = data.Value<string>("cmd");
		Debug.Log("CMD: " + curCmd);

		if(curCmd == "restart")
		{
			setGameState(enGameState.Intro);
		}

		switch (Gvar.gameState)
		{
			case enGameState.None:
				break;
			case enGameState.Intro:
				if (curCmd == "start")
				{
					players.reset();
					setGameState(enGameState.Intro2);
				}
				break;
			case enGameState.Intro2:
				if (curCmd == "ready")
				{
					Debug.Log("Set ready");
					players.setReady(numPlayer);
				}
				if (players.allReady())
					setGameState(enGameState.Play);
				break;
			case enGameState.Play:
				msgResponse r = players.treatMessage(numPlayer, curCmd);

				break;
			case enGameState.Result:

				break;
			case enGameState.End:
				if (curCmd == "end")
					setGameState(enGameState.Intro);
				break;
			default:
				break;
		}
	}







	private void Instance_onDisconnect(int device_id)
	{
		Debug.Log("Disconnected " + device_id);

		players.delete(this, device_id);
		arrangePlayer();
	}

	private void Instance_onDeviceStateChange(int device_id, JToken user_data)
	{
		//	Debug.Log("State change " + device_id + " " + user_data.ToString());
	}

	private void Instance_onCustomDeviceStateChange(int device_id, JToken custom_device_data)
	{
		//Debug.Log("Custom device state change " + device_id + " " + custom_device_data.ToString());
	}

	private void Instance_onConnect(int device_id)
	{
		AirConsole.instance.SetActivePlayers(Gvar.nbPlayerMax);

		Debug.Log("Connected " + device_id);

		playerScript cur = players.add(this, device_id);
		arrangePlayer();

		if (Gvar.gameState == enGameState.Intro)
		{
			AirConsole.instance.Message(device_id, Cmd.Intro + Lng.Intro);
		}
		else if (Gvar.gameState == enGameState.Intro2)
		{
			cur.setRole();
		}
		else
		{
			AirConsole.instance.Message(device_id, Cmd.Intro + Lng.Wait);
		}
	}

	private void arrangePlayer()
	{
		List<playerScript> lstP = players.getList();

		for (int i = 0; i < lstP.Count; i++)
		{
			if (i < 4)
			{
				lstP[i].transform.SetParent(uiPlayLeft.transform);
				RectTransform r = lstP[i].GetComponent<RectTransform>();
				r.offsetMin = new Vector2(0, -110 + i * -115);
				r.offsetMax = new Vector2(0, 0 + i * -115);
				r.localScale = new Vector3(1f, 1f, 1f);
			}
			else
			{
				lstP[i].transform.SetParent(uiPlayRight.transform);
				RectTransform r = lstP[i].GetComponent<RectTransform>();
				r.offsetMin = new Vector2(0, -110 + i * -115);
				r.offsetMax = new Vector2(0, 0 + i * -115);
				r.localScale = new Vector3(1f, 1f, 1f);
			}
		}

	}


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		switch (Gvar.gameState)
		{
			case enGameState.Intro:
				break;
			case enGameState.Play:
				break;
			case enGameState.End:
				break;
			default:
				break;
		}
	}

}
