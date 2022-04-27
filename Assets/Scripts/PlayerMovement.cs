using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D rb;
	float timeUntilShootReady;
	bool crashing;
	GameManager gameManager;
	[SerializeField] BulletPooler bulletPool;
	[SerializeField] Vector2 playerMaxSpeed;
	[SerializeField] Camera playerCamera;

	[SerializeField] float bulletSpawnOffset = 1.0f;
	[SerializeField] float firerate = 1.0f;
	[SerializeField] float bulletSpeed = 100.0f;
	public float reloadTime = 3;
	public float ammoCapacity = 3;
	[SerializeField] uint ammo = 3;

	[SerializeField] uint health = 3;

	[SerializeField] Vector2 maxMovement;
	[SerializeField] Vector2 minMovement;

	[SerializeField] TextMeshProUGUI uiHealth;
	[SerializeField] TextMeshProUGUI uiAmmo;
	
	float reloadTimer;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0.0f;
		timeUntilShootReady = 0.0f;
		reloadTimer = reloadTime;
		crashing = false;

		gameManager = FindObjectOfType<GameManager>();
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
		if (timeUntilShootReady > 0.0f)
		{
			Debug.Log("shoot cooldown");
			return false;
		}

		if (ammo <= 0)
		{
			Debug.Log("no ammo");
			return false;
		}

		return true;
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
			ammo--;
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

	void UpdateUi()
	{
		uiHealth.text = "Health: " + health;
		uiAmmo.text = "Ammo: " + ammo;
	}

	private void HandleReloading()
	{
		if (ammo < ammoCapacity)
		{
			if (reloadTimer < 0.0f)
			{
				ammo++;
				reloadTimer = reloadTime;
			}
			else
			{
				reloadTimer -= Time.deltaTime;
			}
		}
	}
	private void Update()
	{
		if (gameManager.GameActive)
		{
			if (!crashing)
			{
				HandleInput();
			}

			UpdateUi();
			HandleReloading();
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
