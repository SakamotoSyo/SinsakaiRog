using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager
{
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

    public static void NextStageLoad(StageType stageType)
    {
        if (stageType == StageType.Battle)
        {
            SceneManager.LoadScene("MainBattleScene");
        }
        else if (stageType == StageType.Event)
        {
            SceneManager.LoadScene("EventScene");
        }
        else if (stageType == StageType.Shop) 
        {
            SceneManager.LoadScene("ShopScene");
        }
    }
}
