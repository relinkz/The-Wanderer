using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionFX;
    ParticleSystem explosionFxCopy;

    // Start is called before the first frame update
    void Start()
    {
        explosionFxCopy = Instantiate(explosionFX, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explosionFxCopy.transform.position = transform.position;
        explosionFxCopy.Play();

        // using pooling
        gameObject.SetActive(false);
    }
}
