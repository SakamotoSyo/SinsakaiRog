using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ShopButtonScript : MonoBehaviour
{
    [SerializeField] private CardController _cardController;
    [SerializeField] private Image _soldOutImage;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Text _moneyText;
    [Tooltip("Goldが足りなかったときに使う")]
    [SerializeField] private GameObject _warningObj;
    private IPlayerStatus _playerStatus;
    private Action _closeAction;

    private void Start()
    {
        if (_moneyText != null) 
        {
            _moneyText.text = _cardController.CardBaseClass.Gold.ToString();
        }
        _playerStatus = PlayerEventPresenter.PlayerStatus;
    }

    public void Buy() 
    {
        //テストでゴールドを増やす
        _playerStatus.AddGold(100);
        if (_playerStatus.UseGold(_cardController.CardBaseClass.Gold))
        {
            _playerStatus.AddDeckCard(_cardController.CardBaseClass);
            _soldOutImage.enabled = true;
            _shopButton.enabled = false;
        }
        else 
        {
            _warningObj.SetActive(true);
        }
       
    }

    /// <summary>
    /// 除去効果を購入した場合
    /// </summary>
    public void BuyRemovalEffect() 
    {
        //一致するカード名を検索してListから該当したものを一つ削除する
        var searchResult = _playerStatus.GetDeckCardList()
            .Where(e => e.Name == _cardController.CardBaseClass.Name).Distinct().ToArray();

        _playerStatus.GetDeckCardList().Remove(searchResult[0]);
        _closeAction?.Invoke();
    }

    /// <summary>
    /// 強化効果を購入した場合
    /// </summary>
    public void BuyElementEffect()
    {
        var searchResult = _playerStatus.GetDeckCardList()
              .Where(e => e.Name == _cardController.CardBaseClass.Name)
              .Where(e => e.NumberReinforcement == 0)
              .Distinct().ToArray();
        Debug.Log(searchResult[0].Name);
        if (searchResult[0].EnhancementData.CardEnhancement == "Cost")
        {
            searchResult[0].SetEnhancementNum();
            searchResult[0].DecreasedCost();

        }
        else if (searchResult[0].EnhancementData.CardEnhancement == "Power") 
        {
            searchResult[0].SetEnhancementNum();
            searchResult[0].IncreaseEffectPower();
        }
        _closeAction?.Invoke();
    }


    public void SetCloseAction(Action ac)
    {
        _closeAction = ac;
    }
}
