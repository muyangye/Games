using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITypeWriter : MonoBehaviour 
{
	// This script is for the UI Type Writer Effect of the "GameOver" text
	// that shows up at the end of the game

	private Text text;
	public string content;
	public bool gameEnd;

	void Start() 
	{
		text = GetComponent<Text> ();
		content = text.text;
		text.text = "";
		StartCoroutine("AnimateText");
	}


	public IEnumerator AnimateText()
	{
		// Once the game ends, the text is enabled in script and shows up on screen
		// We want to show the animation after the text is enabled
		yield return new WaitUntil(() => text.enabled == true);
		foreach (char character in content) 
		{
			text.text += character;
			yield return new WaitForSeconds (0.10f);
		}
	}

}