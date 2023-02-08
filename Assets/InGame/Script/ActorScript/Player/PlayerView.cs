using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class PlayerView : ActorViewBase
{
    [SerializeField] Text _costText;
    [SerializeField] Text _discardedText;
    [SerializeField] Text _deckText;
    [SerializeField] private GameObject _cardParentObj;
    [SerializeField] private GameObject _cardPrefab;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void DrawView(CardBaseClass card) 
    {
        var obj = Instantiate(_cardPrefab, _cardParentObj.transform.position, Quaternion.identity);
        obj.transform.SetParent(_cardParentObj.transform);
        var cardController = obj.GetComponentInChildren<CardController>();
        cardController.CardAnimation.SetParentTransform(_cardParentObj.transform);
        cardController.SetCardBaseClass(card);
    }
    public void DiscardedCardView(int count) 
    {
       _discardedText.text = count.ToString();
    }

    public void DeckCardView(int count) 
    {
        _deckText.text = count.ToString();
    }

    public void SetCostText(float cost) 
    {
        _costText.text = cost.ToString();
    }

}
