using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCardViewScript : MonoBehaviour
{
    [SerializeField] private PlayerController _playerCon;
    [SerializeField] private GameObject _deckObj;
    [SerializeField] private GameObject _graveyardObj;
    [Tooltip("��������Prefab")]
    [SerializeField] private GameObject _cardPrefab;
    [Tooltip("Prefab�𐶐�����ꏊ")]
    [SerializeField] private GameObject _content;
    [SerializeField] private Text _allViewText;
    [SerializeField] private Scrollbar _scrollbar;
    private List<GameObject> _cardObjList = new();
    /// <summary>
    /// �S�Ẵf�b�L�̃J�[�h��\������
    /// </summary>
    public void AllDeckCardView() 
    {
        _allViewText.text = "�f�b�L";
        var deckCradList = _playerCon.PlayerStatus.GetDeckCardList();
        for (int i = 0; i < deckCradList.Count; i++) 
        {
            var card = Instantiate(_cardPrefab);
            card.GetComponent<CardController>().SetCardBaseClass(deckCradList[i]);
            card.transform.SetParent(_content.transform);
            _cardObjList.Add(card);
        }
       _deckObj.SetActive(true);
    }

    public void CloseDeckCard()
    {
        for (int i = 0; i < _cardObjList.Count; i++) 
        {
            Destroy(_cardObjList[i]);
        }
        _cardObjList.Clear();
        _scrollbar.value = 1;
        _deckObj.SetActive(false);
    }

    /// <summary>
    /// �S�Ă̕�n�̃J�[�h��\������
    /// </summary>
    public void AllGraveyardView() 
    {
        _allViewText.text = "��n";
        var GraveyardCradList = _playerCon.PlayerStatus.GetGraveyardCardList();
        for (int i = 0; i < GraveyardCradList.Count; i++)
        {
            var card = Instantiate(_cardPrefab);
            card.GetComponent<CardController>().SetCardBaseClass(GraveyardCradList[i]);
            card.transform.SetParent(_content.transform);
            _cardObjList.Add(card);
        }
        _deckObj.SetActive(true);
    }
}
