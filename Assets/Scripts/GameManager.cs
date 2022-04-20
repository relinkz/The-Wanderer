using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	bool gameActive = false;
	EnemySpawner enemySpawner;
	GameObject menu;

	public bool GameActive { get => gameActive; }

	// Start is called before the first frame update
	void Start()
	{
		enemySpawner = FindObjectOfType<EnemySpawner>();
		menu = GameObject.Find("Menu");
	}

	void RepeatSpawn()
	{
		enemySpawner.SpawnWave();
	}

	public void StartGame()
	{
		gameActive = true;
		InvokeRepeating("RepeatSpawn", 0, 5);
		menu.SetActive(false);
	}
	// Update is called once per frame
	void Update()
	{

	}


}
