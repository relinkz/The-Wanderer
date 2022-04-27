using UnityEngine;

public class EFollow : MonoBehaviour
{
	GameObject player;
	Enemy enemyData;
	[SerializeField] float speed;
	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		enemyData = GetComponent<Enemy>();
	}

	void ApproachPlayer()
	{
		Vector3 dirToPlayer = player.transform.position - transform.position;
		dirToPlayer.z = 0;
		dirToPlayer.Normalize();

		transform.position += dirToPlayer * speed * Time.deltaTime;
	}
	// Update is called once per frame
	void Update()
	{
		if (enemyData.isAlive == true)
		{
			ApproachPlayer();
		}
	}
}
