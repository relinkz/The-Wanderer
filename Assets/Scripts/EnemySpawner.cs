using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] List<GameObject> prefabs;
	public uint wave;
	public uint waveIncrease;
	uint waveCounter;

	void SpawnWave()
	{
		var enemiesToSpawn = wave;
		for (int i = 0; i < enemiesToSpawn;	i++)
		{
			var spawnPos = Tool.GetRandomPointOnScreenboarder(Tool.ScreenSideGoal.RIGHT);
			Instantiate(prefabs[i%2], spawnPos, prefabs[0].transform.rotation);
		}
		if (waveCounter % waveIncrease == 0 && waveCounter != 0)
		{
			wave++;
			waveCounter = 0;
		}
		waveCounter++;
	}

	// Start is called before the first frame update
	void Start()
	{
		waveCounter = 0;
		InvokeRepeating("SpawnWave", 0, 5 );
	}

	// Update is called once per frame
	void Update()
	{

	}
}
