using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour
{
	public bool isSelected;

	private Color colOri;
	private float var;
	private Renderer curRenderer;

	// Use this for initialization
	void Start ()
	{
		if(GetComponent<Renderer>() != null)
		{
			curRenderer = GetComponent<Renderer>();
		}
		else
		{
			if(transform.childCount > 0)
			{
				for (int i = 0; i < transform.childCount; i++) 
				{
					if(transform.GetChild(i).GetComponent<Renderer>() != null)
					{
						curRenderer = transform.GetChild(i).GetComponent<Renderer>();
						break;
					}
				}
			}
		}

		if(curRenderer != null)
			colOri = curRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(curRenderer == null)
			return;

		var = (Mathf.PingPong(Time.time * 1f, 0.5f));

		if(isSelected)
		{
			curRenderer.material.color = new Color(colOri.r + var, colOri.g + var, colOri.b + var);
		}
		else
		{
			curRenderer.material.color = colOri;
		}
	}
}
