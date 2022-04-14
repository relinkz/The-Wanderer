using UnityEngine;

public static class Tool
{
	public enum ScreenSideGoal
	{
		LEFT = 0x0,
		RIGHT = 0x1,
		TOP = 0x2,
		BOT = 0x3
	}

	public static readonly float leftmostPosition = -11.0f; 
	public static readonly float rightmostPosition = 11.0f;
	public static readonly float downmostPosition = -3.40f;
	public static readonly float upmostPosition = 3.40f;

	public static string ConvertScreenSideGoalToString(ScreenSideGoal screenside)
	{
		if (screenside == ScreenSideGoal.LEFT)
		{
			return "LEFT";
		}
		if (screenside == ScreenSideGoal.RIGHT)
		{
			return "RIGHT";
		}
		if (screenside == ScreenSideGoal.TOP)
		{
			return "TOP";
		}
		if (screenside == ScreenSideGoal.BOT)
		{
			return "BOT";
		}

		return "INVALID";
	}
	public static Vector2 GetRandomPointOnScreenboarder(ScreenSideGoal screenside)
	{
		var targetGoalPoint = Vector2.zero;
		if (screenside == ScreenSideGoal.LEFT)
			targetGoalPoint = new Vector2(leftmostPosition, Random.Range(downmostPosition, upmostPosition));

		else if (screenside == ScreenSideGoal.RIGHT)
			targetGoalPoint = new Vector2(rightmostPosition, Random.Range(downmostPosition, upmostPosition));

		else if (screenside == ScreenSideGoal.BOT)
			targetGoalPoint = new Vector2(Random.Range(leftmostPosition, rightmostPosition), downmostPosition);

		else if (screenside == ScreenSideGoal.TOP)
			targetGoalPoint = new Vector2(Random.Range(leftmostPosition, rightmostPosition), upmostPosition);

		return targetGoalPoint;
	}

}
