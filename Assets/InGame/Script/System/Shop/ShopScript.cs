using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject _shopObj;
    [SerializeField] private GameObject _shopContentPanel;
    [SerializeField] private GameObject _cardContent;
    [SerializeField] private GameObject _shopCardPrefab;
    [Tooltip("カード除去のPrefab")]
    [SerializeField] private GameObject _cardRemoval;
    [SerializeField] private Vector3 _shopPos;
    [SerializeField] private Animator _shopAnim;
    private void Start()
    {

    }

    public async void OpenShop() 
    {
        _shopObj.SetActive(true);
        //Shopのカードを生成
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
            _shopContentPanel.transform.localPosition + _shopPos, 1f);
    }
}
