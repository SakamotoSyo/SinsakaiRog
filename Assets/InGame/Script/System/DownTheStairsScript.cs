using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class DownTheStairsScript : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        playerStatus.LoadPlayerData(GameManager.SaveData);
        NextScene().Forget();
    }

    /// <summary>
    /// 次のシーンに移行する為の処理
    /// </summary>
    private async UniTask NextScene() 
    {
        var token = this.GetCancellationTokenOnDestroy();
        //フェード開始
        await FadeScript.Instance.FadeIn();
        _audioSource.Play();
        //一秒待つ
        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        GameManager.NextCurrentLevel();
     
        await FadeScript.Instance.FadeOut();
        LoadSceneManager.NextStageLoad();
    }
}
