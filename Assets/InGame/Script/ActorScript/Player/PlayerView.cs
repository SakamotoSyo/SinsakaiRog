using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class PlayerView : ActorViewBase
{
    [SerializeField] Text _costText;
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
        var obj = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(_cardParentObj.transform);
        var cardController = obj.GetComponent<CardController>();
        cardController.SetCardBaseClass(card);
    }

    public void SetCost(float cost) 
    {
        _costText.text = cost.ToString();
    }

}
