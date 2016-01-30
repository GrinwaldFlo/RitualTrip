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
	//internal Text txtScoreGlob;
	private Image backgroundImage;
	private clEmotion emotion;
	private int emotionSide;
	private clHero role;

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

	private int p_scoreGlob;
	internal bool ready;

	public int scoreGlob
	{
		get
		{
			return p_scoreGlob;
		}
		set
		{
			if (value != scoreGlob)
			{
				p_scoreGlob = value;
				//txtScoreGlob.text = p_scoreGlob.ToString();
			}
		}
	}


	internal void setScore(int score)
	{

	}

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

		return msgResponse.None;
	}

	internal void setRole()
	{
		role = Gvar.lstHeros[Random.Range(0, Gvar.lstHeros.Count)];
		emotion = Gvar.lstEmotion[Random.Range(0, Gvar.lstEmotion.Count)];
		emotionSide = Random.Range(0, 2);
		AirConsole.instance.Message(deviceId, Cmd.Intro2 + string.Format("Your are {0} {1}", Gvar.addDet(emotion.getText(emotionSide)), role.getLabel()));
	}
}
