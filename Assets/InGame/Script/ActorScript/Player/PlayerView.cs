using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerView : MonoBehaviour
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
        var obj = Instantiate(_cardPrefab, _cardParentObj.transform.position, Quaternion.identity);
        var cardController = obj.GetComponent<CardController>();
        cardController.SetCardBaseClass(card);
    }
}
