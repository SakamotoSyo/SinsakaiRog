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
    [SerializeField] private GameObject _eventObj;
    [SceneName]
    [SerializeField] private string _nextSceneName;
    [SerializeField] private ShopScript _shopScript;
    [Header("�V���b�v���ǂ̒��x�o�����邩")]
    [SerializeField] private int _shopAppears;
    [SerializeField] private IPlayerStatus _playerStatus;
    private void Start()
    {
        _playerStatus = PlayerEventPresenter.PlayerStatus;
        //�Z�[�u����Ă���f�[�^���Z�b�g����
        _playerStatus.SetPlayerSaveData(GameManager.SaveData);
        var num = UnityEngine.Random.Range(0, 101);
        if (num < _shopAppears)
        {
            _shopScript.OpenShop();
        }
        else 
        {
            EventSelect();
        }


    }

    public async void EventSelect() 
    {
        _eventObj.SetActive(true);
        var Event = _eventData.RamdomEventData();
        _eventDescriptionText.text = Event.EventDescription;
        await FadeScript.Instance.FadeIn();
        //���ꂼ��ɃC�x���g��o�^
        _selectButton.onClick.AddListener(() => Event.EventEffect.IfSelected(_eventDescriptionText, Event));
        _selectButton.onClick.AddListener(() =>  NextScene());
        _notSelectButton.onClick.AddListener(() => Event.EventEffect.IfNotSelected());
        _notSelectButton.onClick.AddListener(() => NextScene());
    }

    /// <summary>
    /// �V���b�v���J��
    /// </summary>
    public void OpenShop() 
    {
        
    }

    public void SetDescription() 
    {

    }

    public async void NextScene() 
    {
        _selectButton.enabled = false;
        _notSelectButton.enabled = false;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Debug.Log("���̃V�[����");
        GameManager.SavePlayerData(_playerStatus.GetPlayerSaveData());
        SceneManager.LoadScene(_nextSceneName);
    }
}
