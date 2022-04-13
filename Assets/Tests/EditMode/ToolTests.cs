using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ToolTests
{
	[Test]
	public void ToolTestEnumToStringLeft()
	{
		var resultString = Tool.ConvertScreenSideGoalToString(Tool.ScreenSideGoal.LEFT);
		Assert.AreEqual("LEFT", resultString);
	}
	[Test]
	public void ToolTestEnumToStringRight()
	{
		var resultString = Tool.ConvertScreenSideGoalToString(Tool.ScreenSideGoal.RIGHT);
		Assert.AreEqual("RIGHT", resultString);
	}
	[Test]
	public void ToolTestEnumToStringTop()
	{
		var resultString = Tool.ConvertScreenSideGoalToString(Tool.ScreenSideGoal.TOP);
		Assert.AreEqual("TOP", resultString);
	}
	[Test]
	public void ToolTestEnumToStringBot()
	{
		var resultString = Tool.ConvertScreenSideGoalToString(Tool.ScreenSideGoal.BOT);
		Assert.AreEqual("BOT", resultString);
	}
}
