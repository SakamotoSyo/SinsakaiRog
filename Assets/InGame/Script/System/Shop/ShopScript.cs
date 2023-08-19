using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject _shopObj;
    [SerializeField] private GameObject _shopContentPanel;
    [SerializeField] private GameObject _cardContent;
    [SerializeField] private GameObject _shopCardPrefab;
    [Tooltip("�J�[�h������Prefab")]
    [SerializeField] private GameObject _cardRemoval;
    [SerializeField] private Button _nextSceneButton;
    [SerializeField] private Vector3 _shopPos;
    [SceneName]
    [SerializeField] private string _nextSceneName;
    [SerializeField] private IPlayerStatus _playerStatus;

    private void Start()
    {
        _playerStatus = PlayerEventPresenter.PlayerStatus;
        //�Z�[�u����Ă���f�[�^���Z�b�g����
        _playerStatus.LoadPlayerData(GameManager.SaveData);
        OpenShop().Forget();
        _nextSceneButton.onClick.AddListener(() => NextScene().Forget());
    }

    /// <summary>
    /// Shop�̃J�[�h��I��������ɕ\������
    /// </summary>
    public async UniTask OpenShop() 
    {
        _shopObj.SetActive(true);
        //Shop�̃J�[�h�𐶐�
        for (int i = 0; i < 5; i++)
        {
            var card = Instantiate(_shopCardPrefab);
            card.transform.SetParent(_cardContent.transform);
            var cardInfo = DataBaseScript.GetRandomCard();
            card.GetComponent<CardController>().SetCardBaseClass(cardInfo);

        }
        await FadeScript.Instance.FadeIn();

        DOTween.To(() => _shopContentPanel.transform.localPosition,
            x => _shopContentPanel.transform.localPosition = x,
            _shopContentPanel.transform.localPosition + _shopPos, 1f)
            .SetLink(gameObject);
    }

    public async UniTask NextScene()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
        Debug.Log("���̃V�[����");
        GameManager.SavePlayerData(_playerStatus.GetPlayerSaveData());
        await FadeScript.Instance.FadeOut();
        LoadSceneManager.ToStageMapScene();
    }
}
