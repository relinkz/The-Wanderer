using UnityEngine;

public class EMove : MonoBehaviour
{
	Vector2 travelDir;
	[SerializeField] float speed;
	[SerializeField] Tool.ScreenSideGoal screenDirection;
	Enemy enemyData;

	// Start is called before the first frame update
	void Start()
	{
		travelDir = Tool.GetRandomPointOnScreenboarder(screenDirection);
		travelDir.Normalize();
		enemyData = GetComponent<Enemy>();
	}

	// Update is called once per frame
	void Update()
	{
		if (enemyData.isAlive == true)
		{
			Vector2 newPos = transform.position;
			newPos += travelDir * speed * Time.deltaTime;
			transform.position = newPos;
		}
	}
}
