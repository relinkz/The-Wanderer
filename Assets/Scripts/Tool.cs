using System.Collections;
using System.Collections.Generic;
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
	public static Vector2 GetRandomPointOnScreenboarder()
	{
		return Vector2.zero;
	}

}
