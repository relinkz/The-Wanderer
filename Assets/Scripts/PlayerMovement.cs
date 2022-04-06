using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D m_rb;
  float m_timeUntilShootReady;
  bool crashing;

  [SerializeField] BulletPooler bulletPool;
  [SerializeField] Vector2 playerMaxSpeed;
  [SerializeField] Camera playerCamera;

  [SerializeField] float bulletSpawnOffset = 1.0f;
  [SerializeField] float firerate = 1.0f;

  // Start is called before the first frame update
  void Start()
  {
    m_rb = GetComponent<Rigidbody2D>();
    m_rb.gravityScale = 0.0f;
    m_timeUntilShootReady = 0.0f;
    crashing = false;
  }

  void HandleMovement()
  {
    var xDir = Input.GetAxis("Horizontal");
    var yDir = Input.GetAxis("Vertical");
    
    Vector3 movementForce = Vector2.zero;

    movementForce += Vector3.right * xDir * Time.deltaTime * playerMaxSpeed.x;
    movementForce += Vector3.up * yDir * Time.deltaTime * playerMaxSpeed.y;

    transform.position += movementForce;
  }

  bool CanFire()
  {
    if (m_timeUntilShootReady <= 0.0f)
    {
      return true;
    }
    return false;
  }

  void HandleShooting()
  {
    if (Input.GetMouseButton(0) && CanFire())
    {
      Vector2 mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
      Vector2 playerPos = transform.position;

      Vector2 mouseDir = mousePos - playerPos;
      Vector2 spawnpoint = playerPos + (mouseDir.normalized * bulletSpawnOffset);

      bulletPool.SpawnFriendlyBullet("BasicBullet", spawnpoint, mouseDir.normalized, 100);
      m_timeUntilShootReady = firerate;
    }

    if (m_timeUntilShootReady > 0)
    {
      m_timeUntilShootReady -= Time.deltaTime;
    }
  }

  void HandleInput()
  {
    HandleMovement();
    HandleShooting();
  }

  private void Update()
  {
    if (!crashing)
		{
      HandleInput();
		}
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    crashing = true;
    m_rb.gravityScale = 1.0f;
    m_rb.AddTorque(0.3f, ForceMode2D.Impulse);
    GetComponent<BoxCollider2D>().enabled = false;
  }
}
