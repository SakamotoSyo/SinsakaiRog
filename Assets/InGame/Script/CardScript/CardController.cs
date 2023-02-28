using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using System;


public class CardController : MonoBehaviour
{
    public CardAnimation CardAnimation => _cardAnimation;
    [SerializeField] private CardAnimation _cardAnimation;

    public CardBaseClass CardBaseClass => _cardBaseClass.Value;
    public IObservable<CardBaseClass> CardBaseClassOb => _cardBaseClass;
    private ReactiveProperty<CardBaseClass> _cardBaseClass = new ReactiveProperty<CardBaseClass>();

    private void Awake()
    {
        _cardAnimation.DrawAnim(transform);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ƒJ[ƒh‚ğÌ‚Ä‚éˆ—‚Ì—¬‚ê
    /// </summary>
    public async void ThrowCard() 
    {
       await _cardAnimation.ThrowAnim(transform);
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void SetCardBaseClass(CardBaseClass card) 
    {
        _cardBaseClass.Value = card;
    }
}
