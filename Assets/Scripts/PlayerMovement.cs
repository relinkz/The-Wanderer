using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D m_rb;
  float m_timeUntilShootReady;
  bool m_playerFalling;

  [SerializeField] BulletPooler bulletPool;
  [SerializeField] Vector2 playerMaxSpeed;
  [SerializeField] Camera playerCamera;

  [SerializeField] float accelerationScalar = 1000.0f;
  [SerializeField] float jumpForce = 1000.0f;
  [SerializeField] float bulletSpawnOffset = 1.0f;
  [SerializeField] float firerate = 1.0f;

  // Start is called before the first frame update
  void Start()
  {
    m_rb = GetComponent<Rigidbody2D>();
    m_timeUntilShootReady = 0.0f;
  }

  void HandleMovement()
  {
    var xDir = Input.GetAxis("Horizontal");
    Vector2 movementForce = Vector2.zero;

    if (Mathf.Abs(m_rb.velocity.x) < playerMaxSpeed.x)
    {
      movementForce = Vector2.right * xDir * Time.deltaTime * accelerationScalar;
    }
    if (!m_playerFalling)
    {
      if (Input.GetKeyDown("w"))
      {
        m_playerFalling = true;
        movementForce += Vector2.up * jumpForce;
      }
    }

    m_rb.AddForce(movementForce);
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
      var mouseDir = playerCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      var spawnpoint = transform.position + mouseDir.normalized * bulletSpawnOffset;

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
    HandleInput();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    m_playerFalling = false;
  }
}
