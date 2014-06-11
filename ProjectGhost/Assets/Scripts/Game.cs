using UnityEngine;
using System.Collections;

public static class Game {

	public enum State {Running, Paused};
	// Use this for initialization
	public static State CurrentState;

	private static float timeScaleStored = 1;

	public static void Pause()
	{
		CurrentState = State.Paused;

		timeScaleStored = Time.timeScale;
		Time.timeScale = 0;
	}

	public static void Unpause()
	{
		CurrentState = State.Running;

		Time.timeScale = timeScaleStored;
	}
}
