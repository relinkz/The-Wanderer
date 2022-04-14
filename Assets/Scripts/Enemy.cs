using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] uint healthPoints;

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
		var rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 1;
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			healthPoints--;
			return;
		}

		if (collision.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
			return;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(gameObject);
	}
}
