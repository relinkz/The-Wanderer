using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
  [System.Serializable]
  public class Pool
  {
    public string key;
    public GameObject prefab;
    public uint nrInPool;
  }
  public List<Pool> bulletPool;
  Dictionary<string, Queue<GameObject>> m_friendlyBullets;

  // Start is called before the first frame update
  void Start()
  {
    m_friendlyBullets = new Dictionary<string, Queue<GameObject>>();

    foreach(Pool pool in bulletPool)
    {
      Queue<GameObject> bulletPool = new Queue<GameObject>();

      for (uint i = 0; i < pool.nrInPool; i++)
      {
        var gameobj = Instantiate(pool.prefab);
        gameobj.SetActive(false);
        bulletPool.Enqueue(gameobj);
      }

      m_friendlyBullets.Add(pool.key, bulletPool);
    }
  }

  public void SpawnFriendlyBullet(string tag, Vector2 pos, Vector2 normalizedDir, float force)
  {
    var bullet = m_friendlyBullets[tag].Dequeue();
    bullet.SetActive(true);

    var bulletRb = bullet.GetComponent<Rigidbody2D>();
    bulletRb.velocity = Vector2.zero;

    bullet.transform.position = pos;
    bulletRb.AddForce(normalizedDir * force);

    m_friendlyBullets[tag].Enqueue(bullet);
  }
}
