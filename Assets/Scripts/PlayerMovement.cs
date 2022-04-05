using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D m_rb;
  float m_timeUntilShootReady;

  [SerializeField] BulletPooler m_bulletPool;
  [SerializeField] Vector2 m_playerMaxSpeed;
  [SerializeField] float m_accelerationScalar = 1000;
  [SerializeField] float m_jumpForce = 1000;
  [SerializeField] float m_bulletSpawnOffset = 1;
  [SerializeField] Camera m_camera;
  [SerializeField] float m_firerate;
  bool m_playerFalling;
  // Start is called before the first frame update
  void Start()
  {
    m_rb = GetComponent<Rigidbody2D>();
    m_timeUntilShootReady = 0.0f;
  }

  Vector2 CalculateMovementForcesAndConstrainst()
  {
    var xDir = Input.GetAxis("Horizontal");
    Vector2 test = Vector2.right * xDir * Time.deltaTime * m_accelerationScalar;

    if (Mathf.Abs(m_rb.velocity.x) < m_playerMaxSpeed.x)
    {
      return test;
    }
    else
    {
      return Vector2.zero;
    }
  }

  Vector2 HandleJump()
  {
    if (Input.GetKeyDown("w"))
    {
      m_playerFalling = true;
      return Vector2.up * m_jumpForce;
    }
    return Vector2.zero;
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
      var mouseDir = m_camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      var spawnpoint = transform.position + mouseDir.normalized * m_bulletSpawnOffset;

      m_bulletPool.SpawnFriendlyBullet("BasicBullet", spawnpoint, mouseDir.normalized, 100);
      m_timeUntilShootReady = m_firerate;
    }

    if (m_timeUntilShootReady > 0)
    {
      m_timeUntilShootReady -= Time.deltaTime;
    }
  }

  void HandleInput()
  {
    if (!m_playerFalling)
    {
      var totalForce = CalculateMovementForcesAndConstrainst();
      totalForce += HandleJump();
      m_rb.AddForce(totalForce);
    }

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
