using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager
{
    public static void ToBattleScene() 
    {
        SceneManager.LoadScene("MainBattleScene");
    }

    public static void ToEventScene() 
    {
        SceneManager.LoadScene("EventScene");
    }

    public static void ToTitleScene() 
    {
        SceneManager.LoadScene("Title");
    }

    public static void ToDownTheStairsScene() 
    {
        SceneManager.LoadScene("DownTheStairsScene");
    }
}
