using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public static class GameManager
{
    private const int matchSecondsDuration = 180;

    public static float timer = matchSecondsDuration; 
    public static int currentKeys = 0;
    public static int currentGems = 0;
    public static int currentLives = 5;
    public static int attemps = 1;

}
