using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager
{
    private static StageType _nextStageType;

    public static void ToTitleScene()
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// äKëwà⁄ìÆÇÃÉVÅ[ÉìÇ…à⁄ÇÈ
    /// </summary>
    public static void ToDownTheStairsScene()
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("DownTheStairsScene");
    }

    public static void ToStageMapScene() 
    {
        AudioManager.Instance.Reset();
        SceneManager.LoadScene("StageSelectScene");
    }

    public static void NextStageLoad()
    {
        if (_nextStageType == StageType.Battle)
        {
            SceneManager.LoadScene("MainBattleScene");
        }
        else if (_nextStageType == StageType.Event)
        {
            SceneManager.LoadScene("EventScene");
        }
        else if (_nextStageType == StageType.Shop) 
        {
            SceneManager.LoadScene("ShopScene");
        }
    }

    public static void SetNectStageType(StageType stageType) 
    {
        _nextStageType = stageType;
    }
}
