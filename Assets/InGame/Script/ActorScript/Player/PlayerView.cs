using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class PlayerView : ActorViewBase
{
    [SerializeField] private GameObject _cardParentObj;
    [SerializeField] private GameObject _cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawView(CardBaseClass card) 
    {
        var obj = Instantiate(_cardPrefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(_cardParentObj.transform);
        var cardController = obj.GetComponent<CardController>();
        cardController.SetCardBaseClass(card);
    }

    public override void SetHpCurrent(float currentHp)
    {
        base.SetHpCurrent(currentHp);
    }


}
