using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] uint healthPoints;
	public bool isAlive = true;

	[SerializeField] int dieTorqueMin;
	[SerializeField] int dieTorqueMax;

	// Update is called once per frame
	void Update()
	{
		if (healthPoints == 0)
		{
			StartFalling();
		}
	}

	private void StartFalling()
	{
		isAlive = false;

		var pos = transform.position;
		pos += Vector3.down * Time.deltaTime * 5.0f;
		transform.position = pos;
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (healthPoints <= 0 && collision.gameObject.tag == "Player")
		{
			// player should not collide with a dead enemy
			Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
		}

		if (collision.gameObject.tag == "Bullet")
		{
			healthPoints--;
			return;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
