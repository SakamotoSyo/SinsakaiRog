using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownTheStairsScript : MonoBehaviour
{
    async void Start()
    {
        await FadeScript.Instance.FadeIn();
        GameManager.NextCurrentLevel();
        FadeScript.Instance.FadeOut();
        var num = Random.Range(0, 100);
        if (num < 50)
        {
            LoadSceneManager.ToEventScene();
        }
        else
        {
            LoadSceneManager.ToBattleScene();
        }
    }
}
