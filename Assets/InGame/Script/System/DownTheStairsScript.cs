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
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        playerStatus.LoadPlayerData(GameManager.SaveData);
        NextScene();
    }

    /// <summary>
    /// ���̃V�[���Ɉڍs����ׂ̏���
    /// </summary>
    private async void NextScene() 
    {
        var token = this.GetCancellationTokenOnDestroy();
        //�t�F�[�h�J�n
        await FadeScript.Instance.FadeIn();
        _audioSource.Play();
        //��b�҂�
        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
        GameManager.NextCurrentLevel();
        //���̃V�[���𒊑I
        var num = UnityEngine.Random.Range(0, 100);
        await FadeScript.Instance.FadeOut();

        if (num < 30 && GameManager.CurremtLevel != 1)
        {
            LoadSceneManager.NextStageLoad(StageType.Event);
        }
        else
        {
            LoadSceneManager.NextStageLoad(StageType.Battle);
        }
    }

}
