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
    private List<GameObject> _cardObjList = new();

    /// <summary>
    ///�@������ʂ𔃂����ꍇCradList��\������
    /// </summary>
    public void Buy() 
    {
        _effectText.text = "�ǂ����������?";
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        if (playerStatus.UseGold(_removalCardPrice)) 
        {
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
    }

    public void BuyElement() 
    {
        _effectText.text = "�ǂ����������?";
        var playerStatus = PlayerEventPresenter.PlayerStatus;
        if (playerStatus.UseGold(_elementCardPrice))
        {
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
    }

    /// <summary>
    /// �J���ꂽList�����
    /// </summary>
    public void CloseDeckCard()
    {
        for (int i = 0; i < _cardObjList.Count; i++)
        {
            Destroy(_cardObjList[i]);
            Debug.Log("�폜����");
        }
        _cardObjList.Clear();
        _scrollbar.value = 1;
        _deckCradObj.SetActive(false);
    }
}
