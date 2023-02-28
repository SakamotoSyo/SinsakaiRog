using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class DownTheStairsScript : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    async void Start()
    {
        await FadeScript.Instance.FadeIn();
        _audioSource.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        GameManager.NextCurrentLevel();
        var num = UnityEngine.Random.Range(0, 100);
        if (num < 40)
        {
            LoadSceneManager.ToEventScene();
        }
        else
        {
            LoadSceneManager.ToBattleScene();
        }
    }
}
