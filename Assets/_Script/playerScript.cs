using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class playerScript : MonoBehaviour
{
	internal int deviceId;
	internal int player;
	internal string playerName;
	public Text txtName;
	public Text txtScore;
	private Image backgroundImage;
	internal clEmotion emotion;
	private clHero role;

	internal int answer;
	internal bool win;

	private int p_score;
	public int score
	{
		get
		{
			return p_score;
		}
		set
		{
			if(value != score)
			{
				p_score = value;
				txtScore.text = p_score.ToString();
			}
		}
	}

	internal bool ready;


	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FixedUpdate()
	{

	}

	internal void init(int deviceId)
	{
		this.deviceId = deviceId;
		player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(deviceId);
		playerName = AirConsole.instance.GetNickname(deviceId);

		txtName.text = playerName;
		txtScore.text = score.ToString();

		backgroundImage = this.GetComponent<Image>();
	}



	internal msgResponse treatMessage(string data)
	{
		if(data.StartsWith("But"))
		{
			int.TryParse(data.Substring(3), out answer);
			Debug.Log("Player " + deviceId + " answer: " + answer);
			return msgResponse.None;
		}
		return msgResponse.None;
	}

	internal void setRole()
	{
		if(playersScript.lstHeros.Count == 0)
			playersScript.lstHeros.AddRange(Gvar.lstHeros);
		if (playersScript.lstEmotion.Count == 0)
			playersScript.lstEmotion.AddRange(Gvar.lstEmotion);

		role = playersScript.lstHeros[Random.Range(0, playersScript.lstHeros.Count)];
		emotion = playersScript.lstEmotion[Random.Range(0, playersScript.lstEmotion.Count)];

		playersScript.lstHeros.Remove(role);
		playersScript.lstEmotion.Remove(emotion);

		string roleDescription = string.Format("<b>Your are {0} {1}</b><br>{2}", Gvar.addDet(emotion.getId()), role.getLabel(), emotion.getDescr());
		AirConsole.instance.Message(deviceId, Cmd.Intro2 + roleDescription);
		AirConsole.instance.Message(deviceId, Cmd.Descr + roleDescription);
	}

	internal void sendWinLoose(string txt)
	{
		if (win)
			txt += "You WIN !</b>";
		else
			txt += "You LOOSE !</b>";

		AirConsole.instance.Message(deviceId, txt);
	}

	internal void playSound(clSound curSound)
	{
		AirConsole.instance.Message(deviceId, Cmd.Sound + curSound.name);
	}
}
