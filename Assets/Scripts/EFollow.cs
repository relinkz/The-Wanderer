using UnityEngine;

public class EFollow : MonoBehaviour
{
	GameObject player;
	[SerializeField] float speed;
	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
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
		ApproachPlayer();
	}
}
