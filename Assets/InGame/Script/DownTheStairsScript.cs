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
        var token = this.GetCancellationTokenOnDestroy();
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        playerStatus.SetPlayerSaveData(GameManager.SaveData);
        await FadeScript.Instance.FadeIn();
        _audioSource.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        GameManager.NextCurrentLevel();
        var num = UnityEngine.Random.Range(0, 100);
        await FadeScript.Instance.FadeOut();
        if (num < 30 && GameManager.CurremtLevel != 1)
        {
            LoadSceneManager.ToEventScene();
        }
        else
        {
            LoadSceneManager.ToBattleScene();
        }
    }
}
