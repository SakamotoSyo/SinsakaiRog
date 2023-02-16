using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class EventSceneSelecter : MonoBehaviour
{
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _notSelectButton;
    [SerializeField] private EventDataBase _eventData;
    [SerializeField] private Text _eventDescriptionText;
    [SceneName]
    [SerializeField] private string _nextSceneName;
    private void Start()
    {
        EventSelect();
    }

    public async void EventSelect() 
    {
        //セーブされているデータをセットする
        PlayerEventPresenter.PlayerStatus.SetPlayerSaveData(GameManager.SaveData);
        var Event = _eventData.RamdomEventData();
        _eventDescriptionText.text = Event.EventDescription;
        await FadeScript.Instance.FadeIn();
        //それぞれにイベントを登録
        _selectButton.onClick.AddListener(() => Event.EventEffect.IfSelected());
        _selectButton.onClick.AddListener(() =>  NextScene());
        _notSelectButton.onClick.AddListener(() => Event.EventEffect.IfNotSelected());
        _notSelectButton.onClick.AddListener(() => NextScene());
    }

    public void SetDescription() 
    {

    }

    public async void NextScene() 
    {
        _selectButton.enabled = false;
        _notSelectButton.enabled = false;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Debug.Log("次のシーンへ");
        GameManager.SavePlayerData(PlayerEventPresenter.PlayerStatus.GetPlayerSaveData());
        SceneManager.LoadScene(_nextSceneName);
    }
}
