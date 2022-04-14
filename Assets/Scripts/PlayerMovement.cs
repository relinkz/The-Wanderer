using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D rb;
  float timeUntilShootReady;
  bool crashing;

  [SerializeField] BulletPooler bulletPool;
  [SerializeField] Vector2 playerMaxSpeed;
  [SerializeField] Camera playerCamera;

  [SerializeField] float bulletSpawnOffset = 1.0f;
  [SerializeField] float firerate = 1.0f;
  [SerializeField] float bulletSpeed = 100.0f;
  [SerializeField] Vector2 maxMovement;
  [SerializeField] Vector2 minMovement;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.gravityScale = 0.0f;
    timeUntilShootReady = 0.0f;
    crashing = false;
  }

  Vector2 RestraintMovementFromCamera(Vector3 pos)
	{
    if (pos.x < minMovement.x)
    {
      pos.x = minMovement.x;
    }
    else if (pos.x > maxMovement.x)
    {
      pos.x = maxMovement.x;
    }

    if (pos.y < minMovement.y)
    {
      pos.y = minMovement.y;
    }
    else if (pos.y > maxMovement.y)
    {
      pos.y = maxMovement.y;
    }

    return pos;
  }

  void HandleMovement()
  {
    var xDir = Input.GetAxis("Horizontal");
    var yDir = Input.GetAxis("Vertical");
    
    Vector3 movementForce = Vector2.zero;

    movementForce += Vector3.right * xDir * Time.deltaTime * playerMaxSpeed.x;
    movementForce += Vector3.up * yDir * Time.deltaTime * playerMaxSpeed.y;

    var pos = transform.position;
    pos += movementForce;

    transform.position = RestraintMovementFromCamera(pos);
  }

  bool CanFire()
  {
    if (timeUntilShootReady <= 0.0f)
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

      bulletPool.SpawnFriendlyBullet("BasicBullet", spawnpoint, mouseDir.normalized, bulletSpeed);
      timeUntilShootReady = firerate;
    }

    if (timeUntilShootReady > 0)
    {
      timeUntilShootReady -= Time.deltaTime;
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
    rb.gravityScale = 1.0f;
    rb.AddTorque(0.3f, ForceMode2D.Impulse);
    GetComponent<BoxCollider2D>().enabled = false;
  }
}
