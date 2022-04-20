using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
	Button startButton;
	GameManager gameManager;

	void Start()
	{
		startButton = GetComponent<Button>();
		startButton.onClick.AddListener(ButtonClicked);

		gameManager = FindObjectOfType<GameManager>();
	}

	void ButtonClicked()
	{
		var button = gameObject.name;
		if (button == "Quit")
		{
			Application.Quit();
		}

		if (button == "Start")
		{
			gameManager.StartGame();
		}
	}

}
