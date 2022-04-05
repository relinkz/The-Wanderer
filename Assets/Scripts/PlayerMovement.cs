using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  Rigidbody2D m_rb;

  [SerializeField] Vector2 m_playerMaxSpeed;
  [SerializeField] float m_accelerationScalar = 1000;
  [SerializeField] float m_jumpForce = 1000;
  bool m_playerFalling;
  // Start is called before the first frame update
  void Start()
  {
    m_rb = GetComponent<Rigidbody2D>();
  }

  Vector2 HorizontalMovement()
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

  private void Update()
  {
    if(!m_playerFalling)
    {
      var totalForce = HorizontalMovement();
      totalForce += HandleJump();
      m_rb.AddForce(totalForce);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    m_playerFalling = false;
  }
}
