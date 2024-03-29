using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AuxiliaryShopOpenScript : MonoBehaviour
{
    [SerializeField] private float _removalCardPrice;
    [SerializeField] private float _elementCardPrice;
    [SerializeField] private GameObject _deckCradObj;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _removalCard;
    [SerializeField] private GameObject _elementCard;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Text _effectText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _removalAnim;
    [SerializeField] private Animator _elementAnim;
    private List<GameObject> _cardObjList = new();

    /// <summary>
    ///　特殊効果を買った場合CradListを表示する
    /// </summary>
    public void Buy() 
    {
        _effectText.text = "どれを除去する?";
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        if (playerStatus.UseGold(_removalCardPrice))
        {
            _audioSource.Play();
            var deckCradList = playerStatus.GetDeckCardList();
            for (int i = 0; i < deckCradList.Count; i++)
            {
                var card = Instantiate(_removalCard);
                card.GetComponent<CardController>().SetCardBaseClass(deckCradList[i]);
                card.GetComponent<ShopButtonScript>().SetCloseAction(CloseDeckCard);
                card.transform.SetParent(_content.transform);
                _cardObjList.Add(card);
            }

            _deckCradObj.SetActive(true);
        }
        else 
        {
            _removalAnim.SetTrigger("WarningAnim");
        }
    }

    public void BuyElement() 
    {
        _effectText.text = "どれを強化する?";
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        if (playerStatus.UseGold(_elementCardPrice))
        {
            _audioSource.Play();
            var deckCradList = playerStatus.GetDeckCardList().Where(e => e.NumberReinforcement == 0).ToList();
            for (int i = 0; i < deckCradList.Count; i++)
            {
                var card = Instantiate(_elementCard);
                card.GetComponent<CardController>().SetCardBaseClass(deckCradList[i]);
                card.GetComponent<ShopButtonScript>().SetCloseAction(CloseDeckCard);
                card.transform.SetParent(_content.transform);
                _cardObjList.Add(card);
            }

            _deckCradObj.SetActive(true);
        }
        else 
        {
            _elementAnim.SetTrigger("WarningAnim");
        }
    }

    /// <summary>
    /// 開かれたListを閉じる
    /// </summary>
    public void CloseDeckCard()
    {
        for (int i = 0; i < _cardObjList.Count; i++)
        {
            Destroy(_cardObjList[i]);
            Debug.Log("削除する");
        }
        _cardObjList.Clear();
        _scrollbar.value = 1;
        _deckCradObj.SetActive(false);
    }
}
