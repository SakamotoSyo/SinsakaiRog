using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public CardAnimation CardAnimation => _cardAnimation;
    [SerializeField] private CardAnimation _cardAnimation;

    [SerializeField] private DataBaseScript _dataBaseScript;
    public CardBaseClass CardBaseClass => _cardBaseClass;
    private CardBaseClass _cardBaseClass;

    private void Awake()
    {
        _cardBaseClass = _dataBaseScript.CardBaseClassList[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
