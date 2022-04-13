using UnityEngine;

public class EMove : MonoBehaviour
{
	Vector2 travelDir;
	
	// Start is called before the first frame update
	void Start()
	{
		var screenDirection = Tool.ScreenSideGoal.LEFT;
		Debug.Log("I aim to travel to " + Tool.ConvertScreenSideGoalToString(screenDirection));
	}

	// Update is called once per frame
	void Update()
	{

	}
}
