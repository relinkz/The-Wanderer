using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] ParticleSystem explosionFX;
	ParticleSystem explosionFxCopy;

	// Start is called before the first frame update
	void Start()
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		explosionFxCopy = Instantiate(explosionFX, collision.transform);
		explosionFxCopy.transform.position = transform.position;
		explosionFxCopy.Play();

		// using pooling
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameObject.SetActive(false);
	}
}
