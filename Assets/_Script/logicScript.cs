using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class logicScript : MonoBehaviour
{
	public GameObject introCanvas;
	public GameObject playCanvas;
	public GameObject endCanvas;
	public GameObject uiPlayLeft;
	public GameObject uiPlayRight;
	public GameObject prefabPlayer;

	public Text txtMessageIntro;
	public Text txtMessagePlay;
	public Text txtMessageEnd;

	private float timeStateChange;

	public playerScript[] lstPlayer = new playerScript[Gvar.nbPlayerMax];

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


			switch (state)
			{
				case enGameState.Intro:
					introCanvas.SetActive(true);
					playCanvas.SetActive(false);
					endCanvas.SetActive(false);
					if (AirConsole.instance.GetActivePlayerDeviceIds.Count > 0)
					{
						AirConsole.instance.Broadcast(Cmd.Intro);
						AirConsole.instance.Broadcast(Cmd.IntroText + Lng.Intro);
					}
					break;
				case enGameState.Play:
					introCanvas.SetActive(false);
					endCanvas.SetActive(false);

					AirConsole.instance.Broadcast(Cmd.Play);
					AirConsole.instance.Broadcast(Cmd.PlayText + Lng.Play);
					AirConsole.instance.Broadcast(Cmd.But + "0|" + Lng.Answer1);
					AirConsole.instance.Broadcast(Cmd.But + "1|" + Lng.Answer2);
					AirConsole.instance.Broadcast(Cmd.But + "2|" + Lng.Answer3);
					//AirConsole.instance.Broadcast(Cmd.But + "3|"+ Lng.Answer4);
					break;
				case enGameState.End:
					introCanvas.SetActive(false);
					playCanvas.SetActive(false);
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

	private void Instance_onMessage(int from, JToken data)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);

		if (lstPlayer[numPlayer] == null)
		{
			Debug.Log("Player " + numPlayer + " doesn't exists");
			return;
		}
		string curCmd = data.Value<string>("cmd");
		Debug.Log("CMD: " + curCmd);

		switch (Gvar.gameState)
		{
			case enGameState.None:
				break;
			case enGameState.Intro:

				if(curCmd == "start")
					setGameState(enGameState.Play);
				break;
			case enGameState.Play:
				msgResponse r = lstPlayer[numPlayer].treatMessage(curCmd);
				setGameState(enGameState.End);
				break;
			case enGameState.End:
				if (curCmd == "end")
					setGameState(enGameState.Intro);
				break;
			default:
				break;
		}
	}

	private void resetPlayerScore()
	{
		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstPlayer[i].score = 0;
			}
		}
	}

	private void Instance_onDisconnect(int device_id)
	{
		Debug.Log("Disconnected " + device_id);

		deletePlayer(device_id);
	}

	private void deletePlayer(int device_id)
	{
		int numPlayer = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);

		if (lstPlayer[numPlayer] != null)
		{
			Destroy(lstPlayer[numPlayer].gameObject);
			lstPlayer[numPlayer] = null;
		}
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

		addPlayer(device_id);

		if(Gvar.gameState == enGameState.Intro)
		{
			AirConsole.instance.Message(device_id, Cmd.Intro);
			AirConsole.instance.Message(device_id, Cmd.IntroText + Lng.Intro);
		}
		else
		{
			AirConsole.instance.Message(device_id, Cmd.Intro);
			AirConsole.instance.Message(device_id, Cmd.IntroText + Lng.Wait);
		}
	}

	private void addPlayer(int device_id)
	{
		GameObject newPlayer = Instantiate(prefabPlayer);
		playerScript newScript = newPlayer.GetComponent<playerScript>();
		newScript.init(device_id);
		lstPlayer[newScript.player] = newScript;
		arrangePlayer();
	}

	private void arrangePlayer()
	{
		List<playerScript> lstP = new List<playerScript>();

		for (int i = 0; i < lstPlayer.Length; i++)
		{
			if (lstPlayer[i] != null)
			{
				lstP.Add(lstPlayer[i]);
			}
		}

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
