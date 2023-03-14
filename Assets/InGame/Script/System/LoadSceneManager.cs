using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager
{
    public static void ToBattleScene() 
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("MainBattleScene");
    }

    public static void ToEventScene() 
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("EventScene");
    }

    public static void ToTitleScene() 
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("Title");
    }

    public static void ToDownTheStairsScene() 
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("DownTheStairsScene");
    }
}
