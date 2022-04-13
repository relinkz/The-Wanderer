using NUnit.Framework;

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

	[Test]
	public void ToolTestGetRandomPointOnScreenboarderLeft()
	{
		var resultdir = Tool.GetRandomPointOnScreenboarder(Tool.ScreenSideGoal.LEFT);
		Assert.AreEqual(Tool.leftmostPosition, resultdir.x);

		Assert.GreaterOrEqual(resultdir.y, Tool.downmostPosition);
		Assert.LessOrEqual(resultdir.y, Tool.upmostPosition);
	}

	[Test]
	public void ToolTestGetRandomPointOnScreenboarderRight()
	{
		var resultdir = Tool.GetRandomPointOnScreenboarder(Tool.ScreenSideGoal.RIGHT);
		Assert.AreEqual(Tool.rightmostPosition, resultdir.x);

		Assert.GreaterOrEqual(resultdir.y, Tool.downmostPosition);
		Assert.LessOrEqual(resultdir.y, Tool.upmostPosition);
	}

	[Test]
	public void ToolTestGetRandomPointOnScreenboarderBot()
	{
		var resultdir = Tool.GetRandomPointOnScreenboarder(Tool.ScreenSideGoal.BOT);
		Assert.AreEqual(resultdir.y, Tool.downmostPosition);

		Assert.GreaterOrEqual(resultdir.x, Tool.leftmostPosition);
		Assert.LessOrEqual(resultdir.x, Tool.rightmostPosition);
	}

	[Test]
	public void ToolTestGetRandomPointOnScreenboarderTop()
	{
		var resultdir = Tool.GetRandomPointOnScreenboarder(Tool.ScreenSideGoal.TOP);
		Assert.AreEqual(resultdir.y, Tool.upmostPosition);

		Assert.GreaterOrEqual(resultdir.x, Tool.leftmostPosition);
		Assert.LessOrEqual(resultdir.x, Tool.rightmostPosition);
	}
}
