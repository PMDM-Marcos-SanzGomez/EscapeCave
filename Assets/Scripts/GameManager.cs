using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public static class GameManager
{
	// Duration of a match in seconds
	private const int matchSecondsDuration = 180;

	// Timer representing the current time remaining in the match
	public static float timer = matchSecondsDuration;

	// Counter variable (initially not used in the provided code)
	public static float counter = 0;

	// Number of keys collected by the player
	public static int currentKeys = 0;

	// Number of gems collected by the player
	public static int currentGems = 0;

	// Number of lives remaining for the player
	public static int currentLives = 5;

	// Number of attempts made by the player
	public static int attemps = 1; 


}
