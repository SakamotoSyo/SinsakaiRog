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

    // Start is called before the first frame update
    void Start()
    {
       _cardBaseClass.Value = DataBaseScript.CardBaseClassList[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void SetCardBaseClass(CardBaseClass card) 
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.01));
        _cardBaseClass.Value = card;
    }
}
